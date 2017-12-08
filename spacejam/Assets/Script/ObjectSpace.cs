using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSpace : MonoBehaviour
{
    public int health = 50; //usunąć public jak działa
    public int explosionDamage = 20;
    private const int defaultHealthAfterExplosion = 10;
    public Image objectConditionImage;
    public int precentPerTime = 3;
    public int timeBetweenLostCondition;
    float timer;
    public int chanceToExplode = 0;
    public GameObject explode;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0)
            {
                health = 0;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        timer = timeBetweenLostCondition;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (timer < 0)
        {
            Health -= precentPerTime;
            timer = timeBetweenLostCondition;
            if (Random.Range(0, 100) < chanceToExplode)
            {
                Instantiate(explode, transform.position, transform.rotation);
                if (Health - explosionDamage <= 10)
                {
                    Health = defaultHealthAfterExplosion;
                }
                else
                {
                    Health -= explosionDamage;
                }

            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
