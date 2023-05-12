using UnityEngine;
using UnityEngine.SceneManagement;
public class Invaders : MonoBehaviour
{
    public Invader[] prefabs;
    public int rows = 5;
    public int columns = 11;

    public float spacing = 2.0f;
    public AnimationCurve speed;
    private Vector3 _direction = Vector3.right;

    public int amountKilled { get; private set;}
    public int amountAlive => this.totalInvaders - this.amountKilled;
    public int totalInvaders => this.rows*this.columns;
    public float percentKilled => (float)this.amountKilled/(float)this.totalInvaders;

    public float  missileAttackRate = 1.0f;
    public Projectile missilePrefab;

    private void Awake()
    {
        for(int row = 0; row < this.rows; row++)
        {
            float width = spacing * (this.columns - 1);
            float height = spacing * (this.rows - 1);
            Vector2 centering =  new Vector2(-width/2, -height/2);
            Vector3 rowPosition = new Vector3(centering.x,centering.y +(row * spacing), 0.0f); 
            for(int column = 0; column < this.columns; column++)
            {
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                invader.killed = InvaderKilled;
                Vector3 position = rowPosition;
                position.x += column * spacing;
                invader.transform.localPosition = position;        
            }
        }
    }

    private void Start(){
        InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
    }

    private void Update(){
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy){
                continue;
            }
            if(_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f)) {
                AdvanceRow();
            }else if(_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f)){
                AdvanceRow();
            }
            
        }
    }

    private void AdvanceRow(){
        _direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;

    }

    private void InvaderKilled()
    {
        this.amountKilled++;
        if (this.amountKilled >= this.totalInvaders)
        {
            GameManager.Instance.EndMinigame();
        }
    }

    public void MissileAttack(){
        foreach (Transform invader in this.transform)
        {
            if(!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
            
        }
    }
}
