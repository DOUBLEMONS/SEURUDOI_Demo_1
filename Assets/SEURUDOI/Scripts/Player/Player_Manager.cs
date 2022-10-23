using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager Instance;

    void Awake()
    {
        Instance = this;
    }

    //Move
    [Header("Player_Movement")]
    public float MovePower;
    public float MoveSpeed;
    public bool IsFacingRight = true;
    public Rigidbody2D Rigidbody2D;

    //Jump
    [Header("Player_Jump")]
    public float JumpPower;
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    public bool IsGround;
    public float FallMultiplier;
    public Vector2 VecGravity;
    public float JumpTime;
    public float JumpMultiplier;
    public bool IsJump;
    public float JumpCounter;

    //Animator
    [Header("Player_Animator")]
    public Animator Animator;
    public const string PLAYER_IDLE = "Player_Idle";
    public const string PLAYER_RUN = "Player_Run";
    public const string PLAYER_JUMP = "Player_Jump";
    public string CurrentState;
}
