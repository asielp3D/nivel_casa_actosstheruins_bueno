using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueZone4 : MonoBehaviour
{
    public UIManagerCalle uiManager;

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
