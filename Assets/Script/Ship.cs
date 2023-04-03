using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    Gun[] guns;

    float moveSpeed = 3;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;

    GameObject shield;
    int powerUpGunLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        shield = transform.Find("Shield").gameObject;
        DesactivateShield();
        guns = GetComponentsInChildren<Gun>();
        foreach(Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift);

        shoot = Input.GetKeyDown(KeyCode.Space);
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 2;
        }
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        { 
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x += moveAmount;
        }

        if (moveRight)
        {
            move.x -= moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move;
        /*if (pos.x <= -1f)
        {
            pos.x = -1f;
        }
        if (pos.x >= 18.80f)
        {
            pos.x = 18.80f;
        }
        if (pos.y <= 0)
        {
            pos.y = 0;
        } 
        if (pos.y >= 8)
        {
            pos.y = 8;
        }*/

        transform.position = pos;
    }

    void ActivateShield()
    {
        shield.SetActive(true);
    }

    void DesactivateShield ()
    {
        shield.SetActive(false);
    }

    bool HasShield()
    {
        return shield.activeSelf;
    }

    void AddGuns()
    {
        powerUpGunLevel++;
        foreach(Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement == powerUpGunLevel)
            {
                gun.gameObject.SetActive(true);
            }
        }
    }

    void IncreaseSpeed()
    {
        moveSpeed*=2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (bullet.isEnemy)
            {
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }

        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            if (HasShield())
            {
                DesactivateShield();
            }
            else 
            {
                Destroy(gameObject);
            }
            Destroy(bullet.gameObject);
        }

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            if (powerUp.activateShield)
            {
                ActivateShield();
            }
            if (powerUp.addGuns)
            {
                AddGuns();
            }
            if (powerUp.increaseSpeed)
            {
                IncreaseSpeed();
            }
            Level.instance.AddScore(powerUp.pointValue);
            Destroy(powerUp.gameObject);
        }
    }
}
