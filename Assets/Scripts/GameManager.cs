using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        player = GameObject.FindWithTag("Player");
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
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        StateChanged?.Invoke(newState);
    }
 

    public enum GameState
    {
        InicioGame,
        FalouComChefeFolhaPraia,
        FalouComChefeFolhaVila,
        ChegouVilaPedra
    }
}
