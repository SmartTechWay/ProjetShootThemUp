using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ce script permet � un objet Gun dans le jeu de tirer des balles � intervalles r�guliers, 
 * soit manuellement, soit automatiquement.
 */

public class Gun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;

    public Bullet bullet;
    Vector2 direction;

    public bool autoshoot = false;
    public float shootIntervalSecond = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    public bool isActive = false;

    /* Cette m�thode est appel�e lors de l'initialisation du canon. 
     * Elle d�finit la direction du tir.
     */
    void Start()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    /* Cette m�thode est appel�e � chaque image (frame) du jeu. 
     * Si le canon est actif et est r�gl� pour tirer automatiquement, elle g�re le d�lai et l'intervalle de tir.
     */
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (!autoshoot) return;

        if (delayTimer >= shootDelaySeconds)
        {
            delayTimer = 0;

            if (shootTimer >= shootIntervalSecond)
            {
                Shoot();
                shootTimer = 0;
            }
            else
            {
                shootTimer += Time.deltaTime;
            }
        }
        else
        {
            delayTimer += Time.deltaTime;
        }
    }

    /*Cette m�thode cr�e (instancie) une nouvelle balle � partir du type de balle sp�cifi�, 
     * la place � la position du canon, et lui donne la direction de tir du canon.
     */
    public void Shoot()
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.direction = direction;
    }
}
