using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        GameEvents.current.onLevelStart += TriggerDialogue;
        GameEvents.current.onLevelReload += Continue;
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Continue()
    {
        FindObjectOfType<DialogueManager>().ContinueDialogue(dialogue);
    }
}
