using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureChamber : MonoBehaviour {
    public GameObject door1, door2, door3,door4; //1-2 entrance door, 3-4 inside hub door
    public GameObject[] points;
    public bool outside = false;
    public float timer = 3;
    bool playerStillInside = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerStillInside = true;
        if (outside)
        {
            door2.transform.position = points[2].transform.position;
            door3.transform.position = points[4].transform.position;

            door1.transform.position = points[1].transform.position;
            door4.transform.position = points[7].transform.position;
            outside = false;
        }
        else
        {
            door1.transform.position = points[0].transform.position;
            door4.transform.position = points[6].transform.position;

            door2.transform.position = points[3].transform.position;
            door3.transform.position = points[5].transform.position;
            outside = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerStillInside = false;
        }
    }
}
