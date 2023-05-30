using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Le script "Ship" gère les mouvements et actions d'un vaisseau dans un jeu. 
 * Il permet de contrôler le déplacement du vaisseau, de tirer avec ses armes, 
 * d'activer un bouclier, d'ajouter des armes et d'augmenter sa vitesse.
 * Il gère aussi les collisions avec d'autres objets tels que les balles, 
 * les obstacles destructibles et les bonus de puissance (PowerUps).
 */

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

    /* Cette méthode est appelée lors de l'initialisation du vaisseau. 
     * Elle désactive le bouclier, active tous les canons de niveau 0, et désactive les autres.
     */
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

    /* Cette méthode est appelée à chaque image (frame) du jeu. 
     * Elle vérifie l'entrée du clavier pour déplacer le vaisseau ou pour tirer.
     */
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

    /* Cette méthode est appelée à intervalles de temps réguliers. 
     * Elle gère le mouvement du vaisseau en fonction des touches pressées.
     */
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

    /*ActivateShield(), DesactivateShield(), HasShield(): Ces méthodes activent, 
     * désactivent et vérifient l'état du bouclier.
     */
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

    /*Cette méthode active les canons supplémentaires lorsque 
     * le vaisseau gagne un niveau de puissance.
     */
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

    /*Cette méthode double la vitesse de déplacement du vaisseau.
     */
    void IncreaseSpeed()
    {
        moveSpeed*=2;
    }

    /* Cette méthode est appelée lorsque le vaisseau entre en collision avec un autre objet. 
     * Elle vérifie le type d'objet et agit en conséquence 
     * (par exemple, détruire le vaisseau si c'est une balle ennemie, activer le bouclier si c'est une amélioration de bouclier, etc.).
     */
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
