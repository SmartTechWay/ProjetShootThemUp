using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
{
    public float moveSpeed = 5;

    /* Cette m�thode est appel�e � un taux fixe, ind�pendamment du nombre de frames par seconde. 
     * Ici, elle est utilis�e pour d�placer l'objet vers la gauche en soustrayant la vitesse de 
     * d�placement multipli�e par le temps �coul� depuis la derni�re image (frame) � la position x de l'objet. 
     * Si l'objet d�passe une certaine position � gauche (-2), il est d�truit.
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
