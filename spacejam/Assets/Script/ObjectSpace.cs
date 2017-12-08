using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpace : MonoBehaviour {
    public int health = 50; //usunąć public jak działa
    public Image objectConditionImage;
    public int precentPerTime = 3;
    public int timeBetweenLostCondition;
    float timer;
    public int chanceToExplode=0;
    public GameObject explode;
	// Use this for initialization
	void Start () {
        timer = timeBetweenLostCondition;	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    private void FixedUpdate()
    {
        if (timer < 0)
        {
            health -= precentPerTime;
            timer = timeBetweenLostCondition;
            if (Random.Range(0, 100) < chanceToExplode)
            {
                Instantiate(explode, transform.position, transform.rotation);
                health -= 20;
            }
            if (health < 0) health = 0;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
