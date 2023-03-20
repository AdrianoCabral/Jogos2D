using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemBehavior : MonoBehaviour
{
    public bool nearObject;
    [SerializeField] PlayerInventory.AllItems _itemType; //Escolher qual o tipo de item o objeto vai ser

    void Update(){
        if (Input.GetKeyDown("space") && nearObject) {
            PlayerInventory.Instance.addItem(_itemType);
            Destroy(gameObject);       
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nearObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nearObject = false;
        }
    }
}
