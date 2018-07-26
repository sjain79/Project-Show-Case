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

    bool isShooting;
    bool isFalling;
    bool isTouchingGround;
    bool isJumping;
    bool isAttacking1, isAttacking2, isAttacking3;

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
        if (isDead)
        {
            return;
        }

        if (Input.GetAxis("Player " + playerNumber + " Horizontal") != 0)
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

        if (Input.GetButton("Player " + playerNumber + " Fire 1"))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        if (Input.GetAxis("Player " + playerNumber + " Vertical") > 0 && isTouchingGround)
        {
            isTouchingGround = false;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingSpeed);
            isJumping = true;
        }

        if (Input.GetButtonDown("Player " + playerNumber + " Fire 4"))
        {
            isAttacking3 = true;
        }
        else if (Input.GetButtonDown("Player " + playerNumber + " Fire 3"))
        {
            isAttacking2 = true;
        }
        else if (Input.GetButtonDown("Player " + playerNumber + " Fire 2"))
        {
            isAttacking1 = true;
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

        //myAnimator.SetBool("Shooting", isShooting);


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

        //myAnimator.SetBool("Attack 3", isAttacking3);

        //myAnimator.SetBool("Attack 2", isAttacking2);

        //myAnimator.SetBool("Attack 1", isAttacking1);
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
        myAnimator.SetTrigger("Damage Taken");
    }

    public void CompleteAttack(int attackBoolean)
    {
        switch (attackBoolean)
        {
            case 1:
                isAttacking1 = false;
                break;
            case 2:
                isAttacking2 = false;
                break;
            case 3:
                isAttacking3 = false;
                break;
            default:
                Debug.LogError("Switch out of bound!");
                break;
        }
    }
}
