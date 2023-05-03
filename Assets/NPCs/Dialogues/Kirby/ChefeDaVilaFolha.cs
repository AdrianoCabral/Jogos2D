using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefeDaVilaFolha : MonoBehaviour
{
    public NpcDialogue npcDialogue;
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
        if(state == GameManager.GameState.FalouComChefeFolhaPraia)
        {
            this.transform.position = new Vector3(105,38,0);
            npcDialogue.setconversationStartNode("conversaChefeVila");
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
