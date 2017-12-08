using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnexpectedActionController : MonoBehaviour
{

    public GameObject background;
    public GameObject arrow;
    public GameObject goodPlacement;

    public Transform startPoint;
    public Transform endPoint;

    public Arrow arrowCollision;

    public float arrowSpeed;
    public float fateAction;
    private float actionTimer;

    static bool isEventActionStart = false;
    private bool enabledMomentActionStart = false;
    private bool IsMomentActionStart
    {
        get
        {
            return enabledMomentActionStart;
        }
        set
        {
            enabledMomentActionStart = value;
            background.SetActive(value);
            arrow.SetActive(value);
            goodPlacement.SetActive(value);
        }
    }
    private bool isPlayerPressSpace = false;

    Transform transformPlayer;

    void Start()
    {

    }

    void StartUnexpectedAction()
    {
        ResetTimerAction();
        isEventActionStart = true;
    }

    private void FixedUpdate()
    {
        if (isEventActionStart)
        {
            //Repair
            RunTimerAction();
        }

        if(arrow.activeInHierarchy)
        {
            arrow.transform.Translate(arrowSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if(arrow.transform.position.x>=endPoint.position.x)
        {
            IsMomentActionStart = false;
        }
    }

    void RunTimerAction()
    {
        if (!IsMomentActionStart)
            if (actionTimer <= 0f && !IsMomentActionStart)
            {
                actionTimer = fateAction;
                ManageSpriteAction();
                IsMomentActionStart = true;
                ResetTimerAction();
            }
            else
            {
                actionTimer -= Time.fixedDeltaTime;
            }
    }

    void ManageSpriteAction()
    {
        Debug.Log("Show");
        arrow.transform.position = startPoint.transform.position;
    }

    void ResetTimerAction()
    {
        actionTimer = fateAction;
    }

    private void Update()
    {
        if (IsMomentActionStart)
            CheckInputSpace();

        if(Input.GetButtonDown("Submit"))
        {
            StartUnexpectedAction();
            Debug.Log("GOGOGOGO");
        }
    }

    private void CheckInputSpace()
    {
        if (Input.GetButtonDown("Jump"))
        {
            IsMomentActionStart = false;
            CheckIfArrowHasGoodPlace();
        }
    }

    void CheckIfArrowHasGoodPlace()
    {

        if(arrowCollision.intersect)
        {
            Debug.Log("IT WURKS");
            arrowCollision.intersect = false;
        }
        else
        {
            Debug.Log("Wrong Idiot");
        }
        //arrow placement
        IsMomentActionStart = false;
    }

    void StopUnexpectedAction()
    {
        isEventActionStart = false;
        IsMomentActionStart = false;
        isPlayerPressSpace = false;
    }
}
