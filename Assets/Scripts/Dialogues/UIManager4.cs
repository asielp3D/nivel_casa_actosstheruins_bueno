using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager4 : MonoBehaviour
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
