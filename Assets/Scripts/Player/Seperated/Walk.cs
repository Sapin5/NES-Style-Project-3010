using UnityEngine;

public class Walk : MonoBehaviour
{
    [SerializeField]private float movespeed;
    [SerializeField]private bool touchingGround;
    private Rigidbody2D physicsBody;
    private string currentAction;
    private Transform playerDmgBox, playerArt, normalCollider;
    private bool dashState;
    
    void Awake(){
        playerDmgBox = GetComponent<Transform>().GetChild(0);
        playerArt = GetComponent<Transform>().GetChild(1);
        normalCollider =  GetComponent<Transform>().GetChild(2);
        physicsBody = GetComponent<Rigidbody2D>();

        currentAction = "Idle";

        dashState = GetComponent<Dash>().Dashstate();
    }

    public void Movement(){
        float dir = Input.GetAxis("Horizontal");
        if (dir != 0) {
            playerDmgBox.localPosition = new Vector2(dir > 0 ? 0.2f : -0.2f, 0f);
            normalCollider.localPosition = new Vector2(dir > 0 ? 0f : 0.04f, 0f);
            playerArt.rotation = Quaternion.Euler(0, dir > 0 ? 0 : 180, 0);
            physicsBody.velocity = new Vector2(movespeed*dir, physicsBody.velocity.y);
        }
        if(dir == 0 && touchingGround && dashState){
            physicsBody.velocity = new Vector2(0, physicsBody.velocity.y);
            currentAction = "Idle";
        }else if(touchingGround && dir!=0 && dashState){
            currentAction = "Walking";
        }
    }
}
