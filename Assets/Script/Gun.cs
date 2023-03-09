using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Bullet bullet;

    public bool autoshoot = false;
    public float shootIntervalSecond = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (autoshoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSecond)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
    }
}
