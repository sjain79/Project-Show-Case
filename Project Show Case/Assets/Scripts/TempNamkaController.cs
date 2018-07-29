using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNamkaController : MonoBehaviour
{

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;

    [SerializeField]
    float speed;

    [SerializeField]
    float jumpingSpeed;

    [SerializeField]
    string playerNumber;

    bool isSlashing;
    bool isFalling;
    bool isTouchingGround;
    bool isJumping;
    bool isAttacking1, isAttacking2, isAttacking3;
    bool lockMovement;

    bool isDead;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        isDead = false;
    }

    private void Update()
    {
        PlayerInput();
        SetAnimator();
    }


    private void PlayerInput()
    {
        //Do nothing if player is dead
        //if (isDead)
        //{
        //    return;
        //}

        //Flip the character's sprite based on the player's horizontal axis input and add velocity to it
        if (Input.GetAxis("Player " + playerNumber + " Horizontal") != 0)
        {
            if (!lockMovement)
            {
                if (Input.GetAxis("Player " + playerNumber + " Horizontal") > 0)
                {
                    mySpriteRenderer.flipX = false;
                }
                else if (Input.GetAxis("Player " + playerNumber + " Horizontal") < 0)
                {
                    mySpriteRenderer.flipX = true;
                }

                myRigidbody.velocity = new Vector2(speed * Input.GetAxis("Player " + playerNumber + " Horizontal"), myRigidbody.velocity.y);
            }

            else
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }
        }

        if (Input.GetButton("Player " + playerNumber + " Fire 1") && !isJumping && !isFalling)
        {
            isSlashing = true;
        }
        //else
        //{
        //    isSlashing = false;
        //}

        if (Input.GetAxis("Player " + playerNumber + " Vertical") > 0 && isTouchingGround)
        {
            if (!lockMovement)
            {
                isTouchingGround = false;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingSpeed);
                isJumping = true;
            }
        }

        //if (Input.GetButtonDown("Player " + playerNumber + " Fire 4"))
        //{
        //    isAttacking3 = true;
        //}
        //else if (Input.GetButtonDown("Player " + playerNumber + " Fire 3"))
        //{
        //    isAttacking2 = true;
        //}
        //else if (Input.GetButtonDown("Player " + playerNumber + " Fire 2"))
        //{
        //    isAttacking1 = true;
        //}

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

    private void TakeDamage()
    {
        myAnimator.SetTrigger("Hurt");
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

    public void LockMovement()
    {
        lockMovement = true;
    }

    public void UnlockMovement()
    {
        lockMovement = false;
    }
}
