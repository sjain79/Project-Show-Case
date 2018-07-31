using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour
{

    private void Update()
    {
        if (transform.parent.transform.localPosition.x < 0)
        {
            transform.localPosition = new Vector2(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
        }
        else if (transform.parent.localPosition.x > 0)
        {
            transform.localPosition = new Vector2(Mathf.Abs(transform.localPosition.x), transform.localPosition.y);
        }
    }

}
