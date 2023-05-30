using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    public float moveSpeed = 5;

    /* Cette méthode est appelée à un taux fixe, indépendamment du nombre de frames par seconde. 
     * Ici, elle est utilisée pour déplacer l'objet vers la gauche en soustrayant la vitesse de 
     * déplacement multipliée par le temps écoulé depuis la dernière image (frame) à la position x de l'objet. 
     * Si l'objet dépasse une certaine position à gauche (-2), il est détruit.
     */
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (pos.x < -2)
        {
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
