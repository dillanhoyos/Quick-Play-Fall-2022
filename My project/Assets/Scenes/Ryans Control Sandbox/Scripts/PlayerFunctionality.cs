using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionality : MonoBehaviour
{
    public Rigidbody PlayerBody;

    public bool isAttacking;
    public bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;

        PlayerBody = GetComponent<Rigidbody>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Debug.Log("Attack button PRESSED");
    }

    public void PlayerJump()
    {
        if (!isJumping)
        {
            PlayerBody.velocity += Vector3.up * 5;
            Debug.Log("JUMP PRESSED");
        }
    }
}
