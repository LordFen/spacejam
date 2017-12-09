using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

    public float speedRotation = 10;
    private Vector3 scaleGear;
    private void Start()
    {
        scaleGear = transform.localScale;
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0,  speedRotation * Time.fixedDeltaTime);
        transform.localScale = scaleGear;
    }
}
