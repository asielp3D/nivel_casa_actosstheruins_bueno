using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone7 : MonoBehaviour
{
    public UIManagerMetro uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ToggleDialogueUI(1, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ToggleDialogueUI(1, false);
        }
    }
}
