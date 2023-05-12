using UnityEngine;

public class Bunker : MonoBehaviour
{
    private int _hp = 10;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Missile")){
            this._hp -=2;
            if(_hp <= 0){
                this.gameObject.SetActive(false);
            }
        }else if(other.gameObject.layer == LayerMask.NameToLayer("Invader")){
            this.gameObject.SetActive(false);
        } 
    }
}
