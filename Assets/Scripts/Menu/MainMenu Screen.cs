using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
