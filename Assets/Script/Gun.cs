using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Bullet bullet;
    Vector2 direction;

    public bool autoshoot = false;
    public float shootIntervalSecond = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!autoshoot) return;

        if (delayTimer >= shootDelaySeconds)
        {
            delayTimer = 0;

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


    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}
