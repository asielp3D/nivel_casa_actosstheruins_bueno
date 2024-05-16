using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marbles : MonoBehaviour
{
    public Text marblesCountText;
    private int marblesCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            marblesCount++;
            UpdateMarblesCountText();
        }
    }

    void UpdateMarblesCountText()
    {
        if (marblesCountText != null)
        {
            marblesCountText.text = "Marbles" + marblesCount.ToString(); 
        }
    }

    public void SaveMarblesCount()
    {
        PlayerPrefs.SetInt("Marbles", marblesCount);
        PlayerPrefs.Save();
    }
    
    public void LoadMarblesCount()
    {
        if (PlayerPrefs.HasKey("Marbles"))
        {
            marblesCount = PlayerPrefs.GetInt("Marbles");
            UpdateMarblesCountText();
        }
    }

    private void OnApplicationQuit()
    {
        SaveMarblesCount();
    }

    private void OnDestroy()
    {
        SaveMarblesCount();
    }
}