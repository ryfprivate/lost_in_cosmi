using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject Avatar;
    public GameObject DialogueBox;
    private Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void ContinueDialogue(Dialogue dialogue)
    {
        Avatar.SetActive(true);
        DialogueBox.SetActive(true);
        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation");
        Avatar.SetActive(true);
        DialogueBox.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversaton");
        Avatar.SetActive(false);
        DialogueBox.SetActive(false);
    }
}
