using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Moveme : MonoBehaviour
{
    [SerializeField]private float jumpForce, dashForce, movespeed, duration=0.5f;
    private Rigidbody2D physicsBody;
    public enum Dash {Ready, Dashing, CoolDown, End};
    private Dash dash;
    private Transform playerDmgBox, playerArt;
    [SerializeField]private bool touchingGround, spacePressed;
    private float timer;
    private string currentAction;
    private bool dashing;
    private int direction;
    private RigidbodyConstraints2D originalConstraints;
    
    void Awake(){
        playerDmgBox = GetComponent<Transform>().GetChild(0);
        playerArt = GetComponent<Transform>().GetChild(1);
        physicsBody = GetComponent<Rigidbody2D>();
        currentAction = "Idle";
        originalConstraints = physicsBody.constraints;
        dashing = false;
        direction = 1;
    }

    void Update()
    {
        Attack();
    }

    void FixedUpdate(){
        Jumping();
        Movement();
        DashAbility();
    }

    private void DashAbility(){
        float dir = Input.GetAxis("Horizontal");

        if(dir < 0){
            direction = -1;
        }else if(dir>0){
            direction = 1;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !dashing){
            dash = Dash.Dashing;
            currentAction = "Dashing";
            dashing = true;
        }
        switch(dash){
            case Dash.Ready:
                break;
            case Dash.Dashing:
                physicsBody.velocity = new Vector2((dashForce+movespeed)*direction, physicsBody.velocity.y);

                Timer(duration, Dash.CoolDown);
                break;
            case Dash.CoolDown:
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                physicsBody.constraints = originalConstraints;
                dash = Dash.End;
                break;
            case Dash.End:
                if(Timer(1f,  Dash.Ready)) dashing = false;
                break;
        }
    }
    public bool Timer(float delay, Dash dash){
        timer+=Time.deltaTime;
        // Switches when timer becomes greater than the delay
        if(timer>delay){
            // resets timer
            timer=0;
            // Switches firingState to whatver state was passed
            this.dash = dash;
            return true;
        }else return false;
    }
    

    private void Movement(){
        float dir = Input.GetAxis("Horizontal");
        if (dir != 0) {
            playerDmgBox.localPosition = new Vector2(dir > 0 ? 0.2f : -0.24f, 0f);
            playerArt.rotation = Quaternion.Euler(0, dir > 0 ? 0 : 180, 0);
            physicsBody.velocity = new Vector2(movespeed*dir, physicsBody.velocity.y);
        }
        if(dir == 0 && touchingGround && Dashstate()){
            physicsBody.velocity = new Vector2(0, physicsBody.velocity.y);
            currentAction = "Idle";
        }else if(touchingGround && dir!=0 && Dashstate()){
            currentAction = "Walking";
        }
    }

    private void Attack(){
        if(Input.GetKey(KeyCode.Space) && !spacePressed && Dashstate()){
            spacePressed = true;
        }

        if(spacePressed){
            timer+=Time.deltaTime;
            playerDmgBox.GameObject().SetActive(true);
            currentAction = "Attacking";

            if(timer>=0.3f){
                playerDmgBox.GameObject().SetActive(false);
                spacePressed = false;
                timer = 0;
           }
        }
    }
    
    private void Jumping(){
        if(touchingGround){
            if((Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W))
                            && Dashstate()){
                physicsBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                touchingGround = false;
            }
        }else{
            if(Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)){
                physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            }
        }

        if(physicsBody.velocity.y != 0 && Dashstate()){
            currentAction = "Jumping";
            touchingGround = false;
        }else{
            touchingGround = true;
        }
    }

    private bool Dashstate(){
        return dash != Dash.Dashing && dash != Dash.CoolDown;
    }

    public string Action(){
        return currentAction;
    }
}