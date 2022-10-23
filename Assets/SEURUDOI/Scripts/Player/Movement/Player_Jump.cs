using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    Player_Manager PM;

    void Start()
    {
        PM = GetComponent<Player_Manager>();
        PM.Rigidbody2D = GetComponent<Rigidbody2D>();
        PM.VecGravity = new Vector2(0, -Physics2D.gravity.y);
    }

    void Update()
    {
        Jump();
    }

    void Tumbling()
    {

    }

    void Jump()
    {
        PM.IsGround = Physics2D.OverlapCapsule(PM.GroundCheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, PM.GroundLayer);

        if(Input.GetButtonDown("Jump") && IsGround())
        {
            PM.Rigidbody2D.velocity = new Vector2(PM.Rigidbody2D.velocity.x, PM.JumpPower);
            PM.IsJump = true;
            PM.JumpCounter = 0;
        }

        if(PM.Rigidbody2D.velocity.y > 0 && PM.IsJump)
        {
            PM.JumpCounter += Time.deltaTime;

            if(PM.JumpCounter > PM.JumpTime)
            {
                PM.IsJump = false;
            }

            PM.Rigidbody2D.velocity += PM.VecGravity * PM.JumpMultiplier * Time.deltaTime;
        }

        if(PM.Rigidbody2D.velocity.y < 0)
        {
            PM.Rigidbody2D.velocity -= PM.VecGravity * PM.FallMultiplier * Time.deltaTime;
        }

        if(Input.GetButtonUp("Jump"))
        {
            PM.IsJump=false;
        }
    }

    bool IsGround()
    {
        return Physics2D.OverlapCapsule(PM.GroundCheck.position, new Vector2(1.8f, 0.3f), CapsuleDirection2D.Horizontal, 0, PM.GroundLayer);
    }
}
