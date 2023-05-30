using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ce script fait avancer une balle dans une direction � une certaine vitesse, 
 * et la d�truit apr�s 3 secondes.
 */

public class Bullet : MonoBehaviour
{

    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;

    public Vector2 velocity;

    public bool isEnemy = false;

    /*Cette m�thode est appel�e lors de l'initialisation de la balle. 
     * Elle programme la destruction de l'objet balle apr�s 3 secondes 
     * et garantit que l'objet balle n'est pas d�truit lorsque vous changez de sc�ne.
     */
    void Start()
    {
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    /* Cette m�thode est appel�e � chaque image (frame) du jeu. 
     * Elle calcule la v�locit� de la balle en fonction de sa direction et de sa vitesse.
     */
    void Update()
    {
        velocity = direction * speed;
    }

    /* Cette m�thode est appel�e � intervalles de temps r�guliers. 
     * Elle d�place la balle en fonction de sa v�locit� et du temps �coul� depuis la derni�re image (frame).
     */
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
