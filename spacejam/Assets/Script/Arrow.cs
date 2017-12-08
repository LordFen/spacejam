using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public bool intersect = false;
    // Use this for initialization
    public bool afterInter = false;
    private void OnEnable()
    {
        afterInter = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("And");
        if(collision.CompareTag("Placement"))
        {
            Debug.Log("Inter");
            intersect = true;
            afterInter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Placement"))
        {
            if (!afterInter)
            {
                intersect = false;
                Debug.Log("Out");
            }
        }
    }
}
