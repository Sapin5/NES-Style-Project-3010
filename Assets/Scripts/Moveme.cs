using Unity.VisualScripting;
using UnityEngine;

public class Moveme : MonoBehaviour
{
    [SerializeField]private float jumpForce, dashForce;
    private Rigidbody2D physicsBody;
    public enum Dash {Ready, Dashing,End, CoolDown};
    public Dash dash;
    private Transform player;
    private bool touchingGround;
    private float timer;
    private void Start()
    {
        player = GetComponent<Transform>().GetChild(0);
        physicsBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Attack();
        Jumping();
    }

    void FixedUpdate(){
        Movement();
        DashAbility();
    }

    private void DashAbility(){
        int direction = 1;
        if(transform.rotation.y == -1){
            direction = 1;
        }else if (transform.rotation.y == 0){
            direction = -1;
        }

        switch(dash){
            case Dash.Ready:
                if(Input.GetKey(KeyCode.LeftShift)){
                    dash = Dash.Dashing;
                }
                Debug.Log("Ready to Dash");
                break;
            case Dash.Dashing:
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionY;
                physicsBody.AddForce(dashForce * direction * Vector2.left, ForceMode2D.Impulse);
                Timer(0.1f, Dash.End);
                Debug.Log("Dashing");
                break;
            case Dash.End:
                physicsBody.constraints = RigidbodyConstraints2D.FreezeAll;
                physicsBody.constraints = RigidbodyConstraints2D.None;
                physicsBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                dash = Dash.CoolDown;
                Debug.Log("Dash Ended");
                break;
            case Dash.CoolDown:
                Debug.Log("On Cooldown");
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
            Debug.Log("Hit ground");
        }
    }
}
