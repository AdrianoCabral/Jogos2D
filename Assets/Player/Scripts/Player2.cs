using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _body;
    public float speed = 6f;
    private Vector2 movement;

    // Update is called once per frame

    void Awake()
    {
        this._anim = GetComponent<Animator>();
        this._body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        bool save = Input.GetKeyDown(KeyCode.Alpha1);
        bool load = Input.GetKeyDown(KeyCode.Alpha2);
        if((movX == 0f && movY == 0f) && (movement.x !=0 || movement.y != 0)){
            this._anim.SetFloat("LastHorizontal", movement.x);
            this._anim.SetFloat("LastVertical", movement.y);
        }
        movement.x = movX;
        movement.y = movY;
        this._anim.SetFloat("Horizontal", movement.x);
        this._anim.SetFloat("Vertical", movement.y);
        this._anim.SetFloat("Speed", movement.sqrMagnitude);
        if(save){
            SaveSystem.SavePlayer(this);
        }

        if(load){
            LoadPlayer();
        }
    }

    void FixedUpdate()
    {
        this._body.MovePosition(this._body.position + movement * speed * Time.deltaTime);
    }

    void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    
    void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
        transform.position = position;
    }
}
