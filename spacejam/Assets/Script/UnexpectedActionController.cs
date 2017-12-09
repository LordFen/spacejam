using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnexpectedActionController : MonoBehaviour
{

    public GameObject background;
    public GameObject arrow;
    public GameObject goodPlacement;
    public GameObject gear;
    public Transform startPoint;
    public Transform midPoint;
    public Transform endPoint;
    public AudioSource reparing;
    public Arrow arrowCollision;
    private ObjectSpace currentObjectSpace;

    public delegate void HitDamage();
    public static event HitDamage OnHitDamage;

    public float arrowSpeed;
    public float speedOfRepairing = 4;
    public float fateAction;
    private float actionTimer;

    public static bool isEventActionStart = false;
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
            SetGoodPlacement();
            ScaleGoodPlacement();
            goodPlacement.SetActive(value);
        }
    }

    private void SetGoodPlacement()
    {
        Vector3 newPos = new Vector3(Random.Range(midPoint.position.x, endPoint.position.x), endPoint.transform.position.y, endPoint.transform.position.z);
        goodPlacement.transform.position = newPos;
    }

    private void ScaleGoodPlacement()
    {
        if(GameManager.day<=10)
        {
            goodPlacement.transform.localScale = new Vector3(2, goodPlacement.transform.localScale.y, goodPlacement.transform.localScale.z);
        }
        else if(GameManager.day <= 50)
        {
            goodPlacement.transform.localScale = new Vector3(3, goodPlacement.transform.localScale.y, goodPlacement.transform.localScale.z);
        }
        else if(GameManager.day <= 100)
        {
            goodPlacement.transform.localScale = new Vector3(4, goodPlacement.transform.localScale.y, goodPlacement.transform.localScale.z);
        }
        else if (GameManager.day <= 200)
        {
            goodPlacement.transform.localScale = new Vector3(5, goodPlacement.transform.localScale.y, goodPlacement.transform.localScale.z);
        }
        else if (GameManager.day <= 300)
        {
            goodPlacement.transform.localScale = new Vector3(6, goodPlacement.transform.localScale.y, goodPlacement.transform.localScale.z);
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
        SetGearPosition();
      //  SetPositionAction();
        ResetTimerAction();
        isEventActionStart = true;
    }

    void SetGearPosition()
    {
        gear.transform.position = currentObjectSpace.transform.position;
        gear.SetActive(true);
    }

    void DisableGear()
    {
        gear.SetActive(false);
    }

    void SetPositionAction()
    {
        Vector3 newPositionForAction = new Vector3(currentObjectSpace.transform.position.x, currentObjectSpace.transform.position.y + 1, currentObjectSpace.transform.position.z);
        transform.position = newPositionForAction;
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
            reparing.Play();
            IsMomentActionStart = false;
            CheckIfArrowHasGoodPlace();
        }
    }

    void CheckIfArrowHasGoodPlace()
    {

        if (arrowCollision.intersect)
        {
            arrowCollision.intersect = false;
            Debug.Log("Good");
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
        Debug.Log("End Action");
        DisableGear();
        isEventActionStart = false;
        IsMomentActionStart = false;
        isPlayerPressSpace = false;
        if (currentObjectSpace != null)
            currentObjectSpace.stateObjectSpace = ObjectSpace.StateObjectSpace.Idle;
    }
}
