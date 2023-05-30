using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ce script permet � un objet de se d�placer � une certaine position, 
 * d'activer ses armes et de se d�truire lorsqu'il est touch� par une balle, 
 * tout en cr�ant une explosion et en ajoutant au score du niveau.
 */

public class Destructable : MonoBehaviour
{
    public GameObject explosion;

    bool canBeDestroyed = false;
    public int scoreValue = 100;

    /*� la cr�ation de l'objet, il s'enregistre 
     * aupr�s de l'instance de Level comme un objet destructible.
     */
    void Start()
    {
        Level.instance.AddDestructables();
    }

    /* � chaque image (frame), il v�rifie si l'objet a atteint une certaine position (x < 20). 
     * Si c'est le cas, il est marqu� comme pouvant �tre d�truit et ses armes (si pr�sentes) sont activ�es.
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

    /*Cette m�thode est appel�e quand un autre objet entre dans la zone de collision de cet objet. 
     * Si cet objet est une balle qui n'appartient pas � un ennemi et que l'objet peut �tre d�truit, 
     * il ajoute des points au score du niveau, d�truit la balle et d�truit lui-m�me.
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

    /* Cette m�thode cr�e une explosion � la position de l'objet, 
     * informe le niveau qu'un destructible a �t� enlev� et d�truit l'objet lui-m�me.
     */
    void DestroyShip()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Level.instance.RemoveDestructables();
        Destroy(gameObject);
    }
}
