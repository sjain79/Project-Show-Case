using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamkaController : CharacterScript
{

    bool isSlashing;
    bool isAttacking1, isAttacking2, isAttacking3;

    private void Awake()
    {

    }

    protected override void Update()
    {
        base.Update();
        SetAnimator();
    }


    private void PlayerInput()
    {


        if (Input.GetButton("Player " + playerNumber + " Fire 1") && !isJumping && !isFalling)
        {
            isSlashing = true;
        }

        //TEMPORAL
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }

        //TEMPORAL
        if(Input.GetKeyDown(KeyCode.K))
        {
            Die();
        }

    }

    private void SetAnimator()
    {
        if (Mathf.Abs(myRigidbody.velocity.x) > 0 && Input.GetAxis("Player " + playerNumber + " Horizontal") != 0)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }

        myAnimator.SetBool("Slashing", isSlashing);


        if (Mathf.Round(myRigidbody.velocity.y) < 0)
        {
            isFalling = true;
            isJumping = false;

        }
        else
        {
            isFalling = false;
        }
        myAnimator.SetBool("Falling", isFalling);

        myAnimator.SetBool("Jumping", isJumping);

        myAnimator.SetBool("Dead", isDead);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
            isJumping = false;
        }
    }

    private void Die()
    {
        if (!isDead)
            lockMovement = true;
        else
            lockMovement = false;
        isDead = !isDead;
    }

    public void FinishSlashing()
    {
        isSlashing = false;
    }
}
