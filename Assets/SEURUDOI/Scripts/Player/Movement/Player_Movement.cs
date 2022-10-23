using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Manager PM;

    void Start()
    {
        PM = GetComponent<Player_Manager>();
        PM.Rigidbody2D = GetComponent<Rigidbody2D>();
        PM.Animator = GetComponent<Animator>(); 
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Flip();
    }

    void Move()
    {
        PM.MovePower = Input.GetAxis("Horizontal");

        PM.Rigidbody2D.velocity = new Vector2(PM.MovePower * PM.MoveSpeed, PM.Rigidbody2D.velocity.y);
    }

    void SpeedUp()
    {

    }

    void Flip()
    {
        if(PM.IsFacingRight && PM.MovePower < 0f || !PM.IsFacingRight && PM.MovePower > 0f)
        {
            PM.IsFacingRight = !PM.IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            PM.Rigidbody2D.velocity = Vector3.zero;
        }
    }
}

