using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static float day = 360;
    public float secPerDay;
    float timer;
    public ObjectSpace[] objectSpace;
    public Text dayCounter;
    public Image hubCondition;
    private float hubAverageCondition = 0; //przenieść do FixedUpdate gdy obiekty będą miały health
    private const float maxHealth = 100;
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
        hubAverageCondition = 0;
        for (int i = 0; i < objectSpace.Length; i++)
        {
            hubAverageCondition += objectSpace[i].Health;
        }

        hubCondition.fillAmount = ((float)hubAverageCondition * 100 / (objectSpace.Length * maxHealth)/100);
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
