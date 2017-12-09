using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectSpace : MonoBehaviour
{
    public float health = 50; //usunąć public jak działa
    public int explosionDamage = 20;
    private const int defaultHealthAfterExplosion = 10;
    public Image objectConditionImage;
    public int precentPerTime = 3;
    public int timeBetweenLostCondition;
    float timer;
    public int chanceToExplode = 0;
    public GameObject explode;
    private bool readyToBeRepaired = false;

    public delegate void RepairObjectSpace(ObjectSpace objectSpace);
    public static RepairObjectSpace OnRepairObjectSpace;

    public delegate void StopRepairObjectSpace();
    public static event StopRepairObjectSpace OnStopRepairObjectSpace;
    //   public static event 

    public enum StateObjectSpace
    {
        Idle,
        Repairing
    };

    [HideInInspector]
    public StateObjectSpace stateObjectSpace = StateObjectSpace.Idle;


    public float Health
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

            if (health >= 100)
            {
                stateObjectSpace = StateObjectSpace.Idle;
                health = 100;
            }
            SetHealthBar();
        }
    }

    private void SetHealthBar()
    {
        objectConditionImage.fillAmount = (float)health / 100;
    }

    // Use this for initialization
    void Start()
    {
        Health=health;
        timer = timeBetweenLostCondition;
    }

    private void Update()
    {
        if (readyToBeRepaired && Health < 100 && stateObjectSpace==StateObjectSpace.Idle)
        {
            CheckInputSpace();
        }
    }

    void CheckInputSpace()
    {
        if (Input.GetButtonDown("Jump"))
        {
            stateObjectSpace = StateObjectSpace.Repairing;
            if (OnRepairObjectSpace != null)
            {
                OnRepairObjectSpace(this);
            }
        }
    }

    private void FixedUpdate()
    {
        if (stateObjectSpace==StateObjectSpace.Idle)
            RunDestroyYourselfTimer();
    }

    void RunDestroyYourselfTimer()
    {
        if (timer < 0)
        {
            Health -= precentPerTime;
            timer = timeBetweenLostCondition;
            if (UnityEngine.Random.Range(0, 100) < chanceToExplode)
            {
                Instantiate(explode, transform.position, transform.rotation);
                if (Health>10 && Health - explosionDamage <= 10)
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
            timer -= Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToBeRepaired = true;
            Debug.Log("Press Space");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopBeingRepairig();
        }
    }

    private void StopBeingRepairig()
    {
        if (readyToBeRepaired)
            if (OnStopRepairObjectSpace != null)
                OnStopRepairObjectSpace();
        readyToBeRepaired = false;
        stateObjectSpace = StateObjectSpace.Idle;
        Debug.Log("End Repair");
    }
}
