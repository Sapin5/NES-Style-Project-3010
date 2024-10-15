using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Moveme : MonoBehaviour
{
    [SerializeField] private Vector2 boxSize = new(1f, 1f);
    [SerializeField] private Vector2 boxLoc = new(1f, 1f);
    [SerializeField]private float jumpForce, dashForce, movespeed, duration, coolDown, doubleJumpStr;
    [SerializeField]private bool touchingGround, spacePressed, doubleJump;
    [SerializeField] private enum Dash {Ready, Dashing, CoolDown, End};
    private Dash dash;
    private Rigidbody2D physicsBody;
    private float timer;
    private string currentAction;
    private bool dashing;
    private int direction;
    private Transform playerDmgBox, playerArt, dashCollider, normalCollider;

    private RigidbodyConstraints2D originalConstraints;
    
    void Awake(){
        playerDmgBox = GetComponent<Transform>().GetChild(0);
        playerArt = GetComponent<Transform>().GetChild(1);
        normalCollider =  GetComponent<Transform>().GetChild(2);
        dashCollider = GetComponent<Transform>().GetChild(3);

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
                normalCollider.GameObject().SetActive(false);
                dashCollider.GameObject().SetActive(true);
                physicsBody.velocity = new Vector2((dashForce+movespeed)*direction, physicsBody.velocity.y);
                Timer(duration, Dash.CoolDown);
                break;
            case Dash.CoolDown:
                normalCollider.GameObject().SetActive(true);
                dashCollider.GameObject().SetActive(false);
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                physicsBody.constraints = originalConstraints;
                dash = Dash.End;
                break;
            case Dash.End:
                if(Timer(coolDown,  Dash.Ready)) dashing = false;
                break;
        }
    }

    public void DashTime(float lowerCD){
        if(coolDown>=0.3){
            coolDown-=lowerCD;
        }
    }

    private bool Timer(float delay, Dash dash){
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
            playerDmgBox.localPosition = new Vector2(dir > 0 ? 0.2f : -0.2f, 0f);
            normalCollider.localPosition = new Vector2(dir > 0 ? 0f : 0.04f, 0f);
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
        if(physicsBody.velocity.y < 5 && physicsBody.velocity.y > -100000000){
            DoubleJump();
        }

        if(touchingGround){
            if((Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.W))
                            && Dashstate()){
                physicsBody.velocity = new Vector2(physicsBody.velocity.x, jumpForce);
                touchingGround = false;
                doubleJump = true;
            }
        }else{
            if((Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)) && physicsBody.velocity.y != 0){
                physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            }
        }

        if(physicsBody.velocity.y != 0 && Dashstate()){
            currentAction = "Jumping";
            touchingGround = false;
        }else if(TouchGround()){
            touchingGround = true;
            doubleJump = false;
        }
    }

    private void DoubleJump(){
        if(!touchingGround && doubleJump){
            if((Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.W)) && Dashstate()){
                physicsBody.velocity = new Vector2(physicsBody.velocity.x, jumpForce/doubleJumpStr);
                doubleJump = false;
            }
        }
    }

    private bool TouchGround(){
        RaycastHit2D temp  = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y)-boxLoc, boxSize, 0f, Vector2.down, boxLoc.y);

        if(temp){
            if(temp.transform.CompareTag("Ground")){
                Debug.Log("Collided with " + temp.transform.name);
                return true;
                }else{
                    return false;
                }
        }else{
            Debug.Log("Collided with nothing");
            return false;
        }

    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y)-boxLoc, boxSize);
    }

    private bool Dashstate(){
        return dash != Dash.Dashing && dash != Dash.CoolDown;
    }

    public string Action(){
        return currentAction;
    }
}