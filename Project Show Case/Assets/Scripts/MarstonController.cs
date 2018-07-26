using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarstonController : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;

    GameObject spawnPosition;

    [SerializeField]
    float speed;

    [SerializeField]
    string playerNumber;

    [SerializeField]
    GameObject bullet;

    bool isShooting;
    bool isFalling;
    bool isJumping;
    bool isAttacking;

    bool isDead;
    bool isTouchingGround;

    [SerializeField]
    bool testingBool;

    int health;
    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        spawnPosition = transform.GetChild(0).gameObject;
        //spawnPosition.transform.position = new Vector2(0.75f, 0);

        isDead = false;

        health = 5;
    }

    private void Update()
    {
        SetAnimator();
        if (!testingBool)
            return;
        PlayerInput();
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
                spawnPosition.transform.localPosition = new Vector2((Mathf.Abs(spawnPosition.transform.localPosition.x)), (spawnPosition.transform.localPosition.y));
            }
            else if (Input.GetAxis("Player " + playerNumber + " Horizontal") < 0)
            {
                mySpriteRenderer.flipX = true;
                spawnPosition.transform.localPosition = new Vector2(-(Mathf.Abs(spawnPosition.transform.localPosition.x)), (spawnPosition.transform.localPosition.y));
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
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 12);
            isJumping = true;
        }

        //if (Input.GetButtonDown("Player " + playerNumber + " Fire 4"))
        //{
        //    isAttacking3 = true;
        //}
        //else if (Input.GetButtonDown("Player " + playerNumber + " Fire 3"))
        //{
        //    isAttacking2 = true;
        //}
        if (Input.GetButtonDown("Player " + playerNumber + " Fire 2"))
        {
            isAttacking = true;
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

        //myAnimator.SetBool("Dead", isDead);

        //myAnimator.SetBool("Attack 3", isAttacking3);

        //myAnimator.SetBool("Attack 2", isAttacking2);

        myAnimator.SetBool("Attack", isAttacking);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isTouchingGround = true;
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("There was some collision");

        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Attack Collider"))
        {
            Debug.Log("Collision with attack collider");

            if (collision.gameObject.transform.parent != gameObject)
            {
                Debug.Log("Health reduced");
                health--;
            }
        }
    }

    private void TakeDamage()
    {
        myAnimator.SetTrigger("Damage Taken");
    }

    public void CompleteAttack(int attackBoolean)
    {
        //switch (attackBoolean)
        //{
        //    case 1:
        //        isAttacking1 = false;
        //        break;
        //    case 2:
        //        isAttacking2 = false;
        //        break;
        //    case 3:
        //        isAttacking3 = false;
        //        break;
        //    default:
        //        Debug.LogError("Switch out of bound!");
        //        break;
        //}

        isAttacking = false;
    }

    public void ShootBullet()
    {
        //Debug.Log("Function Called");
        Instantiate(bullet, spawnPosition.transform.position, Quaternion.Euler(0, 0, mySpriteRenderer.flipX ? 180 : 0));
    }
}
