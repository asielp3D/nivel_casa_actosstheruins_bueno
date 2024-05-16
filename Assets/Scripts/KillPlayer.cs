using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public int Death;
    public float delaySeconds = 1f;
    
    public AudioClip mineSound;
    public AudioClip explosionSound;
    public float maxVolumeDistance = 30f;
    public float minVolumeDistance = 5f;

    private AudioSource audioSource;
    private bool hasPlayerEntered = false;
    private Transform playerTransform;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (audioSource.isPlaying)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            float normalizedDistance = Mathf.Clamp01((distanceToPlayer - minVolumeDistance) / (maxVolumeDistance - minVolumeDistance));
            audioSource.volume = 1f - normalizedDistance;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasPlayerEntered)
        {
            hasPlayerEntered = true;
            StartCoroutine(LoadDeathSceneWithDelay());
            PlayExplosionSound();
        }
    }

    IEnumerator LoadDeathSceneWithDelay()
    {
        yield return new WaitForSeconds(delaySeconds);

        SceneManager.LoadScene("Death");
    }

    public void PlayMineSound()
    {
        if(mineSound != null && audioSource != null)
        {
            audioSource.clip = mineSound;
            audioSource.Play();
        }
    }

    public void PlayExplosionSound()
    {
        if(explosionSound != null && audioSource != null)
        {
            audioSource.clip = explosionSound;
            audioSource.Play();
        }
    }
}
