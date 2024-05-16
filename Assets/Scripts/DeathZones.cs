using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZones : MonoBehaviour
{
    public int Death;
    public float delaySeconds = 0.25f;

    private bool hasPlayerEntered = false;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayerEntered)
        {
            hasPlayerEntered = true;
            StartCoroutine(LoadDeathSceneWithDelay());
        }
    }

    IEnumerator LoadDeathSceneWithDelay()
    {
        yield return new WaitForSeconds(delaySeconds);

        SceneManager.LoadScene("Death");
    }
}