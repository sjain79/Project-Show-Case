using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarstonController : CharacterScript
{

    [SerializeField]
    GameObject bullet;

    GameObject muzzleFlash;

    bool isShooting;
    bool isAttacking;

    [SerializeField]
    bool testingBool;

    private void Awake()
    {
        //muzzleFlash = transform.GetChild(3).gameObject;
    }

    protected override void Update()
    {
        base.Update();
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

        //if (Input.GetButton("Player " + playerNumber + " Fire 1"))
        //{
        //    isShooting = true;
        //}
        //else
        //{
        //    isShooting = false;
        //}


        //if (Input.GetAxis("Player " + playerNumber + " Vertical") > 0 && isTouchingGround)
        //{
        //    isTouchingGround = false;
        //    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 12);
        //    isJumping = true;
        //}

        //if (Input.GetButtonDown("Player " + playerNumber + " Fire 4"))
        //{
        //    isAttacking3 = true;
        //}
        //else if (Input.GetButtonDown("Player " + playerNumber + " Fire 3"))
        //{
        //    isAttacking2 = true;
        //}
        if (Input.GetButtonDown("Player " + playerNumber + " Fire 1"))
        {
            isAttacking = true;
        }

    }

    private void SetAnimator()
    {

        //myAnimator.SetBool("Attack 3", isAttacking3);

        //myAnimator.SetBool("Attack 2", isAttacking2);

        myAnimator.SetBool("Attack", isAttacking);
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
                TakeDamage();
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
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
