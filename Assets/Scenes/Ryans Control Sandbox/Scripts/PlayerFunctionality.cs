using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctionality : MonoBehaviour
{
    public bool isAttacking;

    void Start()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Debug.Log("Attack button PRESSED");
    }
}
