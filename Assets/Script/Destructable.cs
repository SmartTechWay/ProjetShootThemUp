using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ce script permet à un objet de se déplacer à une certaine position, 
 * d'activer ses armes et de se détruire lorsqu'il est touché par une balle, 
 * tout en créant une explosion et en ajoutant au score du niveau.
 */

public class Destructable : MonoBehaviour
{
    public GameObject explosion;

    bool canBeDestroyed = false;
    public int scoreValue = 100;

    /*À la création de l'objet, il s'enregistre 
     * auprès de l'instance de Level comme un objet destructible.
     */
    void Start()
    {
        Level.instance.AddDestructables();
    }

    /* À chaque image (frame), il vérifie si l'objet a atteint une certaine position (x < 20). 
     * Si c'est le cas, il est marqué comme pouvant être détruit et ses armes (si présentes) sont activées.
     */
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

    /*Cette méthode est appelée quand un autre objet entre dans la zone de collision de cet objet. 
     * Si cet objet est une balle qui n'appartient pas à un ennemi et que l'objet peut être détruit, 
     * il ajoute des points au score du niveau, détruit la balle et détruit lui-même.
     */
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

    /* Cette méthode crée une explosion à la position de l'objet, 
     * informe le niveau qu'un destructible a été enlevé et détruit l'objet lui-même.
     */
    void DestroyShip()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Level.instance.RemoveDestructables();
        Destroy(gameObject);
    }
}
