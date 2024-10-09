using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Moveme : MonoBehaviour
{
    [SerializeField]private float jumpForce, dashForce, movespeed;
    private Rigidbody2D physicsBody;
    public enum Dash {Ready, Dashing, CoolDown, End};
    public Dash dash;
    private Transform player;
    [SerializeField]private bool touchingGround, spacePressed;
    private float timer;
    private string currentAction;
    private bool dashing = false;

    private RigidbodyConstraints2D originalConstraints;
    
    void Awake(){
        player = GetComponent<Transform>().GetChild(0);
        physicsBody = GetComponent<Rigidbody2D>();
        currentAction = "Idle";
        originalConstraints = physicsBody.constraints;
    }

    void Update()
    {
        Jumping();
        Attack();
    }

    void FixedUpdate(){
        DashAbility();
        Movement();
    }

    private void DashAbility(){
        int direction = transform.rotation.y == 0 ? -1 : 1;
        if(Input.GetKey(KeyCode.LeftShift) && !dashing){
            dash = Dash.Dashing;
            currentAction = "Dashing";
            dashing = true;
        }
        switch(dash){
            case Dash.Ready:
                break;
            case Dash.Dashing:
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionY;
                physicsBody.AddForce(dashForce * direction * Vector2.left, ForceMode2D.Impulse);
                Timer(0.1f, Dash.CoolDown);
                
                break;
            case Dash.CoolDown:
                physicsBody.constraints = RigidbodyConstraints2D.FreezeAll;
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
            transform.rotation = Quaternion.Euler(0, dir > 0 ? 0 : 180, 0);
            transform.Translate((dir > 0 ? dir : -dir) * Time.deltaTime * movespeed, 0, 0);
        /* Reworking Movement, plz dont touch
                    physicsBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0)*movespeed, ForceMode2D.Impulse);
                    if(physicsBody.velocity.magnitude > movespeed)
                    {
                    physicsBody.velocity = physicsBody.velocity.normalized * movespeed;
                    }
        */
        }
        if(dir == 0 && touchingGround && dash != Dash.Dashing && dash != Dash.CoolDown){
            currentAction = "Idle";
        }else if(touchingGround && dir!=0 && dash != Dash.Dashing && dash != Dash.CoolDown){
            currentAction = "Walking";
        }
    }

    private void Attack(){
        if(Input.GetKey(KeyCode.Space) && !spacePressed){
            spacePressed = true;
        }

        if(spacePressed){
            timer+=Time.deltaTime;
            player.GameObject().SetActive(true);
            currentAction = "Attacking";

            if(timer>=0.3f){
                player.GameObject().SetActive(false);
                spacePressed = false;
                timer = 0;
           }
        }
    }
    
    private void Jumping(){
        if(touchingGround){
            if((Input.GetKey(KeyCode.UpArrow)|| 
                    Input.GetKey(KeyCode.W)) && dash != Dash.Dashing){
                physicsBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                touchingGround = false;
            }
        }else{
            if(Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)){
                physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            }
        }

        if(physicsBody.velocity.y != 0){
            currentAction = "Jumping";
            touchingGround = false;
        }else{
            touchingGround = true;
        }
    }

    public string Action(){
        return currentAction;
    }

}
