using Unity.VisualScripting;
using UnityEngine;

public class Moveme : MonoBehaviour
{
    [SerializeField]private float jumpForce, dashForce, movespeed;
    private Rigidbody2D physicsBody;
    public enum Dash {Ready, Dashing,End, CoolDown};
    public Dash dash;
    private Transform player;
    private bool touchingGround, spacePressed;
    private float timer;
    private string currentAction, lastAction;

    void Awake(){
        currentAction = "Idle";
    }
    private void Start()
    {
        player = GetComponent<Transform>().GetChild(0);
        physicsBody = GetComponent<Rigidbody2D>();
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

        switch(dash){
            case Dash.Ready:
                if(Input.GetKey(KeyCode.LeftShift)){
                    dash = Dash.Dashing;
                }
                //Debug.Log("Ready to Dash");
                break;
            case Dash.Dashing:
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionY;
                physicsBody.AddForce(dashForce * direction * Vector2.left, ForceMode2D.Impulse);
                currentAction = "Dashing";
                Timer(0.1f, Dash.End);
                //Debug.Log("Dashing");
                break;
            case Dash.End:
                physicsBody.constraints = RigidbodyConstraints2D.FreezeAll;
                physicsBody.constraints = RigidbodyConstraints2D.None;
                physicsBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                dash = Dash.CoolDown;
                //Debug.Log("Dash Ended");
                break;
            case Dash.CoolDown:
                //Debug.Log("On Cooldown");
                Timer(1f, Dash.Ready);
                break;
        }
    }
    public void Timer(float delay, Dash dash){
        timer+=Time.deltaTime;
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
            //physicsBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0)*movespeed, ForceMode2D.Impulse);
            transform.Translate((dir > 0 ? dir : -dir) * Time.deltaTime * movespeed, 0, 0);
        }else if(dir == 0 && touchingGround){
            currentAction = "Idle";
        }

        if(touchingGround && dir!=0){
            currentAction = "Walking";
        }
    }

    private void Attack(){
        if(Input.GetKey(KeyCode.Space) && !spacePressed){
            spacePressed = true;
            lastAction = currentAction;
        }

        if(spacePressed){
            timer+=Time.deltaTime;
            player.GameObject().SetActive(true);
            currentAction = "Attacking";

            if(timer>=0.3f){
                player.GameObject().SetActive(false);
                currentAction = lastAction;
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
                currentAction = "Jumping";
            }
        }else{
            if(Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S)){
                physicsBody.AddForce(Vector2.down/2, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Square") touchingGround = true;
   
    }

    public string Action(){
        return currentAction;
    }
}
