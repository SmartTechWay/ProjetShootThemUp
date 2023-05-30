using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Ce script Unity fait clignoter le sprite (l'image) d'un bouclier dans le jeu. 
 * A chaque image (frame) du jeu, il inverse l'état d'affichage du bouclier. 
 * Si le bouclier est visible, il le rend invisible, et s'il est invisible, il le rend visible.
 */

public class Shield : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;


    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
}
