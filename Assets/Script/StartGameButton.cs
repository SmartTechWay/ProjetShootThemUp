using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public int gameStartScene;
    
    /* C'est une m�thode publique qui peut �tre attach�e au bouton comme un �v�nement dans l'�diteur Unity. 
     * Lorsque le bouton est cliqu�, cette m�thode sera appel�e.
     */
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
