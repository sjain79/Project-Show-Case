using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;

    [SerializeField]
    float speed;

    bool isShooting;
    bool isFalling;
    bool isTouchingGround;
    bool isJumping;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PlayerInput();
        SetAnimator();
    }


    private void PlayerInput()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                mySpriteRenderer.flipX = false;
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                mySpriteRenderer.flipX = true;
            }

            myRigidbody.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), myRigidbody.velocity.y);
        }

        if (Input.GetButton("Fire1"))
        {
            isShooting=true;
        }
        else
        {
            isShooting = false;
        }

        if (Input.GetAxis("Vertical")>0 && isTouchingGround)
        {
            isTouchingGround = false;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 5);
            isJumping = true;
        }
    }

    private void SetAnimator()
    {
        if (Mathf.Abs(myRigidbody.velocity.x) > 0 && Input.GetAxis("Horizontal") != 0)
        {
            myAnimator.SetBool("Running", true);
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }

        myAnimator.SetBool("Shooting", isShooting);


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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
            isJumping = false;
        }
    }
}
