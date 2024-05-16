using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone13 : MonoBehaviour
{
    public UIManagerCasas uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ToggleDialogueUI(0, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ToggleDialogueUI(0, false);
        }
    }
}
