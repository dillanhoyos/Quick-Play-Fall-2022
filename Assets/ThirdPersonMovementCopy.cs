using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovementCopy : MonoBehaviour
{
    public CharacterController controller;
    public Joystick joystick;
    public Transform cam;
    public Rigidbody PlayerBody;

    public float speed = 10f;

    // Update is called once per frame
    void Start()
    {
        PlayerBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerBody.velocity = new Vector3(0, 0, 0);
    }

    void PlayerMovement()
    {
        PlayerBody.velocity = new Vector3(joystick.Horizontal * speed, joystick.Vertical, PlayerBody.velocity.z);
    }

    void PlayerJump()
    {
        PlayerBody.velocity += Vector3.up * 5;
    }
}
