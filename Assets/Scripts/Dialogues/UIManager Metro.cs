using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerMetro : MonoBehaviour
{
    public GameObject generalUI;
    public GameObject[] dialogueUIs;

    void Start()
    {
        foreach (GameObject dialogueUI in dialogueUIs)
        {
            dialogueUI.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DialogueZone1"))
        {
            ToggleDialogueUI(0, true);
            ToggleDialogueUI(1, false);
        }
        if (other.CompareTag("DialogueZone2"))
        {
            ToggleDialogueUI(0, false);
            ToggleDialogueUI(1, true);
        }
       
    }

    public void ToggleDialogueUI(int dialogueIndex, bool showDialogue)
    {
        foreach (GameObject dialogueUI in dialogueUIs)
        {
            dialogueUI.SetActive(false);
        }

        dialogueUIs[dialogueIndex].SetActive(showDialogue);

        generalUI.SetActive(!showDialogue);
    }
}
