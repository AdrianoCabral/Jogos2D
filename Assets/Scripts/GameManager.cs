using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> StateChanged;

    private GameObject player;

    public GameObject BarreiraVilaFolha;
    public GameObject BarreiraVilaPedra;
    public GameObject BarreiraArvoreSagrada;
    public GameObject BarreiraEntradaVilaPedra;
    public GameObject TriggerConversaChefes;

    public NpcDialogue npcDialogue;

    private GameObject chefeTriboPedra;
    private GameObject chefeTriboFolha;
    public CinemachineVirtualCamera _camera;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        player = GameObject.FindWithTag("Player");
        chefeTriboPedra = GameObject.FindWithTag("ChefeTriboPedra");
        chefeTriboFolha = GameObject.FindWithTag("ChefeTriboFolha");
    }

    private void Start()
    {
        UpdateGameState(GameState.InicioGame);
    }


    // Update is called once per frame
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch(newState)
        {
            case GameState.InicioGame:
                TriggerConversaChefes.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.FalouComChefeFolhaPraia:
                BarreiraVilaFolha.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.FalouComChefeFolhaVila:
                BarreiraVilaPedra.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.ChegouVilaPedra:
                player.transform.position = new Vector3(-72, 40, 0);
                BarreiraEntradaVilaPedra.GetComponent<Collider2D>().enabled = false;
                break;
            case GameState.posMiniGame:
                chefeTriboPedra.transform.position = new Vector3(-13, 9, 0);
                chefeTriboFolha.transform.position = new Vector3(-2, 9, 0);
                TriggerConversaChefes.GetComponent<Collider2D>().enabled = true;
                break;
            case GameState.posConversaChefes:
                chefeTriboPedra.transform.position = new Vector3(-86, 48, 0);
                _camera.LookAt = chefeTriboPedra.transform;
                _camera.Follow = chefeTriboPedra.transform;
                StartCoroutine(waiterDialogue("DeVoltaATriboDosPedras"));

                break;
            case GameState.cenaFinal:
                chefeTriboPedra.transform.position = new Vector3(-13, 9, 0);
                _camera.LookAt = player.transform;
                _camera.Follow = player.transform;
                StartCoroutine(waiterDialogue("CenaFinal"));
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        StateChanged?.Invoke(newState);
    }

    IEnumerator waiterDialogue(String dialogue)
    {
        npcDialogue.SetconversationStartNode(dialogue);
        yield return new WaitForSeconds(1);
        npcDialogue.StartConversa();

    }


    public enum GameState
    {
        InicioGame,
        FalouComChefeFolhaPraia,
        FalouComChefeFolhaVila,
        ChegouVilaPedra,
        posMiniGame,
        posConversaChefes,
        cenaFinal
    }
}
