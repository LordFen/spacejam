using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float HP = 100;
    public float damageFromObjectSpace = 20;
    float speed = 5;
    float speedtoHelth = 1;
    public float bleedingTime;
    float timer;
    public GameObject blood;
    Vector3 PlayerPos;


    // Use this for initialization
    void Start()
    {
        timer = bleedingTime;
        PlayerPos = transform.position;
    }

    private void OnEnable()
    {
        UnexpectedActionController.OnHitDamage += GetDamage;
    }

    private void OnDisable()
    {
        UnexpectedActionController.OnHitDamage -= GetDamage;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        float xPos = transform.position.x + (Input.GetAxis("Horizontal") * speed * speedtoHelth * Time.deltaTime);
        float yPos = transform.position.y + (Input.GetAxis("Vertical") * speed * speedtoHelth * Time.deltaTime);
        PlayerPos = new Vector3(xPos, yPos, 0);
        transform.position = PlayerPos;

        if (HP < 50)
        {
            if (timer < 0)
            {
                GameObject bloods = (GameObject)Instantiate(blood, transform.position, transform.rotation);
                Destroy(bloods, 2f);
                timer = bleedingTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if (HP < 80)
        {
            speedtoHelth = HP / 100;
            if (speedtoHelth < 0.3)
            {
                speedtoHelth = (float)0.3;
            }
        }
        else
        {
            speedtoHelth = 1;
        }
        if (HP <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void GetDamage()
    {
        HP -= damageFromObjectSpace;
    }
}
