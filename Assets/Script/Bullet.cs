using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ce script fait avancer une balle dans une direction à une certaine vitesse, 
 * et la détruit après 3 secondes.
 */

public class Bullet : MonoBehaviour
{

    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;

    public Vector2 velocity;

    public bool isEnemy = false;

    /*Cette méthode est appelée lors de l'initialisation de la balle. 
     * Elle programme la destruction de l'objet balle après 3 secondes 
     * et garantit que l'objet balle n'est pas détruit lorsque vous changez de scène.
     */
    void Start()
    {
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    /* Cette méthode est appelée à chaque image (frame) du jeu. 
     * Elle calcule la vélocité de la balle en fonction de sa direction et de sa vitesse.
     */
    void Update()
    {
        velocity = direction * speed;
    }

    /* Cette méthode est appelée à intervalles de temps réguliers. 
     * Elle déplace la balle en fonction de sa vélocité et du temps écoulé depuis la dernière image (frame).
     */
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos += velocity * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
