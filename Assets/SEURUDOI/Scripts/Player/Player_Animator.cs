using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    Player_Manager PM;

    void Start()
    {
        PM = GetComponent<Player_Manager>();
    }

    void ChangeAnimationState(string NewState)
    {
        //Stop the same animation from interrupting itself
        if (PM.CurrentState == NewState) return;

        //Play the animation
        PM.Animator.Play(NewState);

        //Reassign the current state
        PM.CurrentState = NewState;
    }

    void Update()
    {
        if (PM.IsGround == false)
        {
            ChangeAnimationState("Player_Jump");
        }
        else if(PM.MovePower != 0)
        {
            ChangeAnimationState("Player_Run");
        }
        else
        {
            ChangeAnimationState("Player_Idle");
        }
    }
}
