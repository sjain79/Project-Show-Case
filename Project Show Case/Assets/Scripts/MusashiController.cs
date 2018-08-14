using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusashiController : CharacterScript {

    bool isSlashing;
    bool isAttacking1, isAttacking2, isAttacking3;

    private void Awake()
    {

    }

    protected override void Update()
    {
        base.Update();
        PlayerInput();
        SetAnimator();
    }


    private void PlayerInput()
    {
        if (isDead)
            return;

        if (Input.GetButton("Player " + playerNumber + " Fire 1") && !isJumping && !isFalling)
        {
            isSlashing = true;
        }

        //TEMPORAL
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }

        //TEMPORAL
        if (Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }

    }

    private void SetAnimator()
    {
        myAnimator.SetBool("Special", isSlashing);
    }
}
