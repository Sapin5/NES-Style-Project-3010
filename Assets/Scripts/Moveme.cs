using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class Moveme : MonoBehaviour
{
    [SerializeField]private float jumpForce, dashForce;
    private Rigidbody2D physicsBody;

    public enum Dash {Ready, Dashing,End, CoolDown};
    public Dash dash;
    private Transform player;
    private bool touchingGround;
    private float timer, target;
    private void Start()
    {
        player = GetComponent<Transform>().GetChild(0);
        physicsBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Attack();
        Movement();
        Jumping();
    }

    void FixedUpdate(){
        DashAbility();
    }

    private void DashAbility(){
        int direction = 1;
        if(transform.rotation.y == -1){
            Debug.Log("Facing left");
            direction = 1;
        }else if (transform.rotation.y == 0){
            Debug.Log("Facing Right "  + transform.rotation.y);
            direction = -1;
        }

        switch(dash){
            case Dash.Ready:
                if(Input.GetKeyDown(KeyCode.LeftShift)){
                //transform.position = new Vector3(transform.position.x+2f, transform.position.y, 0);
                //physicsBody.AddForce(Vector2.right*4, ForceMode2D.Impulse);
                    dash = Dash.Dashing;
                    target = Time.time+0.1f;
                }
                break;
            case Dash.Dashing:
                
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionY;
                physicsBody.AddForce(dashForce * direction * Vector2.left, ForceMode2D.Impulse);
                Timer(0.1f, Dash.End, target);
                break;
            case Dash.End:
                physicsBody.constraints = RigidbodyConstraints2D.FreezeAll;
                physicsBody.constraints = RigidbodyConstraints2D.None;
                physicsBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                dash = Dash.CoolDown;
                target = Time.time+1f;
                break;
            case Dash.CoolDown:
                Timer(1f, Dash.Ready, target);
                break;
        }
    }
    public void Timer(float delay, Dash dash, float target){
        timer+=Time.deltaTime;
        Debug.Log($"{timer} time passed. Target {delay}");
        // Switches when timer becomes greater than the delay
        if(timer>delay){
            // resets timer
            timer=0;
            // Switches firingState to whatver state was passed
            this.dash = dash;
        }
    }

    private void Movement(){
        float dir = Input.GetAxis("Horizontal");

        if (dir != 0) {
            transform.rotation = Quaternion.Euler(0, dir > 0 ? 0 : 180, 0);
            transform.Translate((dir > 0 ? dir : -dir) * Time.deltaTime * 10, 0, 0);
        }
    }

    private void Attack(){
        if(Input.GetKey(KeyCode.Space)){
            player.GameObject().SetActive(true);
        }else{
            player.GameObject().SetActive(false);
        }
    }
    
    private void Jumping(){
        if((Input.GetKeyDown(KeyCode.UpArrow)|| 
                Input.GetKeyDown(KeyCode.W))&& touchingGround){
            physicsBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            touchingGround = false;
        }

        if((Input.GetKey(KeyCode.DownArrow)|| 
                Input.GetKey(KeyCode.S))&& !touchingGround){
            physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            Debug.Log("Going Down");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Square"){
            touchingGround = true;
        }
    }
}
