using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float speed;

    private void FixedUpdate()
    {
        transform.Translate(transform.right * speed * 0.1f, Space.World);
    }

    private void OnBecameVisible()
    {
        Debug.Log("Called Destroy on bullet");
        //Destroy(gameObject);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
