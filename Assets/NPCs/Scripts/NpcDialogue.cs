using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcDialogue : MonoBehaviour
{

    public string[] dialogueNpc;
    public int dialogueIndex;

    //public GameObject dialoguePanel;
    public Text dialogueText;

    public Dialogue dialogue;

    public Text nameNpc;
    public Image imageNpc;
    public Sprite spriteNpc;

    public bool readyToSpeak;
    public bool startDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //dialoguePanel.SetActive(false);
        this.dialogue = new Dialogue("Kirby", "/NPCs/Dialogues/Kirby/dialogue.txt");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") && readyToSpeak) {
            
            if (!startDialogue) {
                FindObjectOfType<Player2>().speed = 0f;
                StartDialogue();
            }else{
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
        }
        
    }

    void StartDialogue() 
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //nameNpc.text = "Kirby";
        //imageNpc.sprite = spriteNpc;
        startDialogue = true;
        //dialogueIndex = 0;
        //dialoguePanel.SetActive(true);
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
}
