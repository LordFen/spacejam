using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introtext : MonoBehaviour {

    private void Awake()
    {
        Time.timeScale = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
	}
}
