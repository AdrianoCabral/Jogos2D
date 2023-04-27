using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> StateChanged;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
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
            case GameState.FalouComChefePraia:
                break;
            case GameState.FalouComChefeVila:
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        StateChanged?.Invoke(newState);
    }
 

    public enum GameState
    {
        InicioGame,
        FalouComChefePraia,
        FalouComChefeVila,
    }
}
