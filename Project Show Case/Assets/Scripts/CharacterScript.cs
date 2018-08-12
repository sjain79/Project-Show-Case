using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D myRigidbody;
    [SerializeField]
    protected Animator myAnimator;

    SpriteRenderer mySpriteRenderer;
    GameObject spawnPosition;

    public int playerNumber;
    public int health;
    public int speed;
    public int jumpingSpeed;

    protected bool isTouchingGround;
    protected bool isDead;
    protected bool lockMovement;
    protected bool isJumping;
    protected bool isFalling;


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
    }

    protected virtual void Update()
    {
        SetPlayerMovementInput();
    }

    void SetPlayerMovementInput()
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
        }
    }

    private void SetHorizontal()
    {
        if (Input.GetAxis("Player " + playerNumber + " Horizontal") != 0)
        {
            mySpriteRenderer.flipX = IsFacingLeft();
            myRigidbody.velocity = new Vector2(speed * Input.GetAxis("Player " + playerNumber + " Horizontal"), myRigidbody.velocity.y);
            UpdateSpawnPosition();
        }
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
}
