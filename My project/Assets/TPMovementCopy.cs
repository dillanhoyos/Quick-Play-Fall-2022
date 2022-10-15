using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPMovementCopy : MonoBehaviour
{
    protected Joystick joystick;

    void Start()
    {
        var PlayerBody = GetComponent<CharacterController>();
        joystick = FindObjectofType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    { 
        PlayerBody.velocity = new Vector3(joystick.Horizontal * 100f, PlayerBody.velocity.y, joystick.Vertical * 100f);
    }
}
