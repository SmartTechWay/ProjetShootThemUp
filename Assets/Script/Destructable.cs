using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public GameObject explosion;

    bool canBeDestroyed = false;
    public int scoreValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        Level.instance.AddDestructables();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 20f && !canBeDestroyed)
        {
            canBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns )
            {
                gun.isActive = true;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);

        if (!canBeDestroyed)
        {
            return;
        }

        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet && !bullet.isEnemy)
        {
            Level.instance.AddScore(scoreValue);
            Destroy(bullet.gameObject);
            DestroyShip();
        }
    }

    private void OnDestroy()
    {
        //Level.instance.RemoveDestructables();
    }

    void DestroyShip()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Level.instance.RemoveDestructables();
        Destroy(gameObject);
    }
}
