using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public int gameStartScene;
    
    /* C'est une méthode publique qui peut être attachée au bouton comme un événement dans l'éditeur Unity. 
     * Lorsque le bouton est cliqué, cette méthode sera appelée.
     */
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
