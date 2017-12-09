﻿using System.Collections;
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
    private ObjectSpace currentObjectSpace;

    public delegate void HitDamage();
    public static event HitDamage OnHitDamage;

    public float arrowSpeed;
    public float speedOfRepairing = 2;
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

    private void OnEnable()
    {
        ObjectSpace.OnRepairObjectSpace += StartUnexpectedAction;
        ObjectSpace.OnStopRepairObjectSpace += StopUnexpectedAction;
    }

    private void OnDisable()
    {
        ObjectSpace.OnRepairObjectSpace -= StartUnexpectedAction;
        ObjectSpace.OnStopRepairObjectSpace -= StopUnexpectedAction;
    }


    void StartUnexpectedAction(ObjectSpace objSpace)
    {
        currentObjectSpace = objSpace;
        ResetTimerAction();
        isEventActionStart = true;
    }

    private void FixedUpdate()
    {
        if (isEventActionStart)
        {
            RepairObjectSpace();
            RunTimerAction();
        }

        if (arrow.activeInHierarchy)
        {
            arrow.transform.Translate(arrowSpeed * Time.fixedDeltaTime, 0, 0);
        }

        if (arrow.transform.position.x >= endPoint.position.x)
        {
            StopUnexpectedAction();
            if (OnHitDamage != null)
                OnHitDamage();
        }
    }

    void RepairObjectSpace()
    {
        currentObjectSpace.Health += Time.fixedDeltaTime * speedOfRepairing;
        if (currentObjectSpace.Health >= 100)
            StopUnexpectedAction();
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

        if (arrowCollision.intersect)
        {
            arrowCollision.intersect = false;
        }
        else
        {
            StopUnexpectedAction();
            if (OnHitDamage != null)
                OnHitDamage();
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
        currentObjectSpace.stateObjectSpace = ObjectSpace.StateObjectSpace.Idle;
    }
}
