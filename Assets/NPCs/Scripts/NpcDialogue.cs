using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public bool chegouVilaPedra;

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
        FindObjectOfType<Player2>().speed = 50f;
        isCurrentConversation = false;
        }
        if (GameManager.Instance.State == GameManager.GameState.InicioGame)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.FalouComChefeFolhaPraia);
        }else if (GameManager.Instance.State == GameManager.GameState.FalouComChefeFolhaPraia)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.FalouComChefeFolhaVila);
        }

        else if (GameManager.Instance.State == GameManager.GameState.ChegouVilaPedra)
        {
           GameManager.Instance.StartMinigame();
        }
        else if (GameManager.Instance.State == GameManager.GameState.posConversaChefes)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.reuniaoDasTribos);
        } else if (GameManager.Instance.State == GameManager.GameState.reuniaoDasTribos)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.ultimaConversa);
        } else if (GameManager.Instance.State == GameManager.GameState.ultimaConversa)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.creditos);
        }

        return;

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

    public void SetconversationStartNode(String conversa)
    {
        conversationStartNode = conversa;
    }

    public void StartConversa()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
            dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        }
    }
    public void SetChegouVilaDePedra()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
            dialogueRunner.onDialogueComplete.AddListener(EndConversationBarreiraVila);
        }

    }

    private void EndConversationBarreiraVila()
    {
        if (isCurrentConversation)
        {
            FindObjectOfType<Player2>().speed = 10f;
            isCurrentConversation = false;
        }
            GameManager.Instance.UpdateGameState(GameManager.GameState.ChegouVilaPedra);
    }

    public void conversaChefes()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            StartConversation();
            dialogueRunner.onDialogueComplete.AddListener(EndConversationEntreChefes);
        }

    }
    private void EndConversationEntreChefes()
    {
        if (isCurrentConversation)
        {
            FindObjectOfType<Player2>().speed = 10f;
            isCurrentConversation = false;
        }
        GameManager.Instance.UpdateGameState(GameManager.GameState.posConversaChefes);
    }
}
