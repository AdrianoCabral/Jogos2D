using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby : MonoBehaviour
{
    void Awake()
    {
        GameManager.StateChanged += HandleStageChange;
    }

    void OnDestroy()
    {
        GameManager.StateChanged -= HandleStageChange;
    }

    void HandleStageChange(GameManager.GameState state)
    {
        if(state == GameManager.GameState.FalouComChefePraia)
        {
            this.transform.position = new Vector3(100,30,0);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
