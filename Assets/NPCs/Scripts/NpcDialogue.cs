using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
public class NpcDialogue : MonoBehaviour
{

    public string[] dialogueNpc;
    public int dialogueIndex;

    //public GameObject dialoguePanel;
    public Text dialogueText;
    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    public bool readyToSpeak;
    public bool startDialogue;

    private DialogueRunner dialogueRunner;
    private bool isCurrentConversation;

    public string conversationStartNode;

    // Start is called before the first frame update
    void Start()
    {
        //dialoguePanel.SetActive(false);
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && readyToSpeak) {

            if (!dialogueRunner.IsDialogueRunning)
            {

                StartConversation();
                dialogueRunner.onDialogueComplete.AddListener(EndConversation);
            }
  
        }
        
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
        FindObjectOfType<Player2>().speed = 6f;
        isCurrentConversation = false;
            GameManager.Instance.UpdateGameState(GameManager.GameState.FalouComChefePraia);
        }
    }

    private void StartConversation()
    {
        FindObjectOfType<Player2>().speed = 0f;
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            readyToSpeak = false;
        }
    }

    public void setconversationStartNode(String conversa)
    {
        conversationStartNode = conversa;
    }
}
