using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    float day = 360;
    public float secPerDay;
    float timer;
    public GameObject[] objectSpace;
    public Text dayCounter;
    public Image hubCondition;
    public float hubAverageCondition = 0; //przenieść do FixedUpdate gdy obiekty będą miały health
    // Use this for initialization
    void Start () {
        timer = secPerDay;
        dayCounter.text = "Days to rescue: " + day.ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
        /*for (int i = 0; i < objectSpace.Length; i++)
        {
            //hubAverageCondition += objectSpace[i].health;
        }*/

        hubCondition.fillAmount = (float) hubAverageCondition / objectSpace.Length/100;
        if (timer < 0)
        {
            day--;
            dayCounter.text = "Days to rescue: " + day.ToString();
            timer = secPerDay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
