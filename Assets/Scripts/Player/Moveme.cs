using Unity.VisualScripting;
using UnityEngine;

public class Moveme : MonoBehaviour
{
    [Header("Box Collider Settings:")]
    [SerializeField] private Vector2 boxSize = new(1f, 1f);
    [SerializeField] private Vector2 boxLoc = new(1f, 1f);
    [SerializeField] private Vector2 boxLocHead = new(1f, 1f);

    [Header("Jumping Properties:")]
    [SerializeField] private float jumpForce;
    [SerializeField] private bool touchingGround;
    [SerializeField] private bool spacePressed;

    [Header("Movement Speed:")]
    [SerializeField] private float movespeed;

    [Header("Dash Properties:")]
    [SerializeField] private float duration;
    [SerializeField] private float coolDown;
    [SerializeField] private float dashForce;
    [SerializeField] private enum Dash {Ready, Dashing, CoolDown, End};
    private Dash dash;
    private Rigidbody2D physicsBody;
    private float timer;
    private string currentAction;
    private bool dashing;
    private int direction;
    private Transform playerDmgBox, playerArt, dashCollider, normalCollider;
    private RigidbodyConstraints2D originalConstraints;

    private PlayerHealth health;
    private Shield shield;
    void Awake(){
        playerDmgBox = GetComponent<Transform>().GetChild(0);
        playerArt = GetComponent<Transform>().GetChild(1);
        normalCollider =  GetComponent<Transform>().GetChild(2);
        dashCollider = GetComponent<Transform>().GetChild(3);
        physicsBody = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        shield = GetComponent<Shield>();

        currentAction = "Idle";
        originalConstraints = physicsBody.constraints;
        dashing = false;
        direction = 1;
    }

    void Update()
    {
        if(health.RemainingHealth() > 0){
            Attack();
            Crouch();
        }
        
    }

    void FixedUpdate(){
        if(health.RemainingHealth() > 0){
            Jumping();
            Movement();
            DashAbility();
        }
    }

    private void DashAbility(){
        float dir = Input.GetAxis("Horizontal");

        if(dir < 0){
            direction = -1;
        }else if(dir>0){
            direction = 1;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !dashing && !spacePressed){
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
                playerArt.localPosition = new Vector2(0f, 0.08f);
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

    public bool Dashing(){
        return dashing;
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
            if(!Crouch()){
                currentAction = "Idle";
            }
        }else if(touchingGround && dir!=0 && Dashstate()){
            if(!Crouch()){
                currentAction = "Walking";
            }
        }
    }

    private bool Crouch(){
        if(Jammed() && TouchGround() && Dashstate()){
            playerArt.localPosition = new Vector2(0, 0.15f);
            normalCollider.GameObject().SetActive(false);
            dashCollider.GameObject().SetActive(true);
            currentAction = "Crouching";
            return true;
        }else if(Dashstate()){
            playerArt.localPosition = new Vector2(0, 0.17f);
            normalCollider.GameObject().SetActive(true);
            dashCollider.GameObject().SetActive(false);
            return false;
        }
        return false;
        
    }

    private bool Jammed(){
        RaycastHit2D temp  = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y)+boxLocHead, boxSize, 0f, Vector2.down, boxLocHead.y);

        if(temp){
            if(temp.transform.CompareTag("CrouchCollider")){
                return true;
                }else{
                    return false;
                }
        }else{
            return false;
        }
    }

    private void Attack(){
        if(Input.GetKey(KeyCode.Space) && !spacePressed && Dashstate()){
            spacePressed = true;
            playerArt.GetComponent<AnimationSwapper>().StopParticles();
            playerArt.GetComponent<Animator>().SetTrigger("Attack");
        }

        if(spacePressed){
            timer+=Time.deltaTime;
            playerDmgBox.GameObject().SetActive(true);
            if(timer>0.4f){
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
                physicsBody.velocity = new Vector2(physicsBody.velocity.x, jumpForce);
                touchingGround = false;
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
        }
    }

    private bool TouchGround(){
        RaycastHit2D temp  = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y)-boxLoc, boxSize, 0f, Vector2.down, boxLoc.y);

        

        if(temp){
            Debug.Log($"Temp {temp.transform.name}");
            if(temp.transform.CompareTag("Ground") ||
               temp.transform.CompareTag("CrouchCollider"))
            {
                return true;
                }else{
                    return false;
                }
        }else{
            return false;
        }

    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y)-boxLoc, boxSize);
        Gizmos.DrawCube(new Vector2(transform.position.x, transform.position.y)+boxLocHead, boxSize);
    }

    private bool Dashstate(){
        return dash != Dash.Dashing;
    }

    public string Action(){
        return currentAction;
    }
}