using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Ajustes : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
