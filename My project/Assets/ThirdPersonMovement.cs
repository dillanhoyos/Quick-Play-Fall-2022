using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Joystick joystick;
    public Transform cam;
 
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;


    private Vector3 _playerVelocity;

    private bool _groundedPlayer;
    [SerializeField] private float _jumpHeight = 5.0f;

    private bool _jumpPressed = false;
    private float _gravityValue = -9.81f;


    ///JUMP
    public float jumpspeed = 5;
    public bool isGrounded;


    void Start()
    {
        controller = GetComponent<CharacterController>();
     
   

       
    }

    // Update is called once per frame
    void Update()
    {   
   

        Joystick();
        MovementJump();

    }




    public void Joystick()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
         
        if(direction.magnitude >=0.1f)

        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

    }

    public void PlayerJump()
    {

        //Check if no vertical movement
        if(controller.velocity.y == 0)
        {
            Debug.Log("CanJump");
            _jumpPressed = true;


        }
        else
        {
            Debug.Log("noJump");
        }
           
    }

    void MovementJump()
    {
        _groundedPlayer = controller.isGrounded;

        if (_groundedPlayer)
        {
            _playerVelocity.y = 0.0f;
        }
        if (_jumpPressed && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -1* _gravityValue);
            _jumpPressed = false;

        }

        _playerVelocity.y += _gravityValue * Time.deltaTime;
        controller.Move(_playerVelocity * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}

