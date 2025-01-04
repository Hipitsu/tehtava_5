using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    } // ChangeScene

    public void QuitGame()
    {
        Application.Quit();
    } // QuitGame

} // Class
