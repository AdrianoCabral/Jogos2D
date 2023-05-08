using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barreiras : MonoBehaviour
{
    public NpcDialogue npcDialogue;
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(go.name == "BarreiraEntradaVilaPedra")
            {
            npcDialogue.SetChegouVilaDePedra();

            }
            else if (go.name == "TriggerConversaChefes")
            {
                npcDialogue.conversaChefes();
            }
        } 
    }
}
