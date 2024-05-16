using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    private GameObject player;

    public void RestartButton()
    {
        SceneManager.LoadScene("NivelCalle");
    }
    
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}
