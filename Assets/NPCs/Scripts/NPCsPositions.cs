using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsPositions : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.StateChanged += GameManager_StateChanged;
    }

    private void GameManager_StateChanged(GameManager.GameState state)
    {
        if(state ==  GameManager.GameState.FalouComChefeFolhaPraia)
        gameObject.transform.position = new Vector3 (105, 40, 0);
    }

}
