using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _body;
    public float speed = 5f;
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
        if((movX == 0f && movY == 0f) && (movement.x !=0 || movement.y != 0)){
            this._anim.SetFloat("LastHorizontal", movement.x);
            this._anim.SetFloat("LastVertical", movement.y);
        }
        movement.x = movX;
        movement.y = movY;
        this._anim.SetFloat("Horizontal", movement.x);
        this._anim.SetFloat("Vertical", movement.y);
        this._anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        this._body.MovePosition(this._body.position + movement * speed * Time.deltaTime);
    }
}
