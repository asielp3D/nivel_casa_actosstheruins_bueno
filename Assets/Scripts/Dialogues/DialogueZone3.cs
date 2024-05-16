using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone3 : MonoBehaviour
{
    public UIManagerPrueba uiManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ToggleDialogueUI(2, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.ToggleDialogueUI(2, false);
        }
    }
}
