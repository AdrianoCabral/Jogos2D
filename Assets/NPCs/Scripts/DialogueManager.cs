using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    public GameObject dialoguePanel;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        this.dialoguePanel.transform.Find("DialogueText").GetComponent<Text>().text = sentence;
        dialoguePanel.SetActive(true);
        Debug.Log(sentence);
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("cabo o papo");
    }
}
