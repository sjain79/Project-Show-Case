using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D myRigidbody;
    [SerializeField]
    protected Animator myAnimator;
    protected SpriteRenderer mySpriteRenderer;
    protected GameObject spawnPosition;


    public int playerNumber;
    public int health;
    public int speed;
    public int jumpingSpeed;

    protected bool isTouchingGround;
    protected bool isDead;
    protected bool lockMovement;
    protected bool isJumping;
    protected bool isFalling;
    protected bool isRunning;
    protected bool isAttacking;
    protected bool isAttacking2;
    protected bool isAttacking3;
    protected bool isSpecial;


    // Update is called once per frame
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        isTouchingGround = true;
        isDead = false;
        lockMovement = false;
        isJumping = false;
        isSpecial = false;
        spawnPosition = transform.GetChild(0).gameObject;
    }

    protected virtual void Update()
    {
        PlayerInput();
        SetBasicAnimator();
    }

    void PlayerInput()
    {
        if (!isDead)
        {
            if (!lockMovement)
            {
                SetHorizontal();
                SetVertical();
            }

            else
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }

            if (Input.GetButtonDown("Player " + playerNumber + " Fire 1"))
            {
                if (!isAttacking && !isAttacking2 && !isAttacking3)
                    isAttacking = true;

                else if(isAttacking && !isAttacking2)
                {
                    isAttacking2 = true;
                }

                else if(isAttacking2)
                {
                    isAttacking3 = true;
                }
            }

            if (Input.GetButton("Player " + playerNumber + " Fire 2") && !isJumping && !isFalling)
            {
                isSpecial = true;
            }

            //TEMPORAL
            if (Input.GetKeyDown(KeyCode.H))
            {
                TakeDamage();
            }

            //TEMPORAL
            if (Input.GetKeyDown(KeyCode.K))
                Die();
        }
    }

    void SetBasicAnimator()
    {
        isRunning = (Mathf.Round(Mathf.Abs(myRigidbody.velocity.x)) > 0 && Input.GetAxis("Player " + playerNumber + " Horizontal") != 0);

        isFalling = (Mathf.Round(myRigidbody.velocity.y) < 0);

        isJumping = isFalling ? false : isJumping;

        myAnimator.SetBool("Running", isRunning);

        myAnimator.SetBool("Falling", isFalling);

        myAnimator.SetBool("Jumping", isJumping);

        myAnimator.SetBool("Attack", isAttacking);

        myAnimator.SetBool("Attack 2", isAttacking2);

        myAnimator.SetBool("Attack 3", isAttacking3);

        myAnimator.SetBool("Special", isSpecial);

        myAnimator.SetBool("Dead", isDead);
    }

    private void SetHorizontal()
    {
        //if (Input.GetAxis("Player " + playerNumber + " Horizontal") != 0)
        //{
        //    mySpriteRenderer.flipX = IsFacingLeft();
        //    myRigidbody.velocity = new Vector2(speed * (Input.GetAxis("Player " + playerNumber + " Horizontal") / Mathf.Abs(Input.GetAxis("Player " + playerNumber + " Horizontal"))), myRigidbody.velocity.y);
        //    UpdateSpawnPosition();
        //}

        //else
        //{
        //    myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
        //}

        mySpriteRenderer.flipX = Input.GetAxis("Player " + playerNumber + " Horizontal") != 0 ? IsFacingLeft() : mySpriteRenderer.flipX;
        myRigidbody.velocity = new Vector2(speed * DirectionMultiplier(), myRigidbody.velocity.y);
        UpdateSpawnPosition();


    }

    private int DirectionMultiplier()
    {
        return (int)(Input.GetAxis("Player " + playerNumber + " Horizontal") / Mathf.Abs(Input.GetAxis("Player " + playerNumber + " Horizontal")));
    }

    private void SetVertical()
    {
        if (Input.GetButton("Player " + playerNumber + " Jump") && isTouchingGround)
        {
            isTouchingGround = false;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpingSpeed);
            isJumping = true;
        }
    }

    private void UpdateSpawnPosition()
    {
        if (IsFacingLeft())
            spawnPosition.transform.localPosition = new Vector2((Mathf.Abs(spawnPosition.transform.localPosition.x)), (spawnPosition.transform.localPosition.y));

        else
            spawnPosition.transform.localPosition = new Vector2(-(Mathf.Abs(spawnPosition.transform.localPosition.x)), (spawnPosition.transform.localPosition.y));
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
            Die();
        else
            myAnimator.SetTrigger("Damage Taken");

    }

    public void Die()
    {
        isDead = true;
    }

    bool IsFacingLeft()
    {
        return Input.GetAxis("Player " + playerNumber + " Horizontal") < 0;
    }

    public void LockMovement()
    {
        lockMovement = true;
    }

    public void UnlockMovement()
    {
        lockMovement = false;
    }

    public void FinishAttacks()
    {
        isAttacking = false;
        isAttacking2 = false;
        isAttacking3 = false;
        isSpecial = false;
        UnlockMovement();
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
