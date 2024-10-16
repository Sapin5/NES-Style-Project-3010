using Unity.VisualScripting;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]private float dashForce, movespeed, duration, coolDown;
    [SerializeField]private bool spacePressed;
    [SerializeField] private enum DashAb {Ready, Dashing, CoolDown, End};
    private DashAb dash;
    private Rigidbody2D physicsBody;
    private float timer;
    private bool dashing;
    private int direction;
    private Transform dashCollider, normalCollider;

    private RigidbodyConstraints2D originalConstraints;
    
    void Awake(){
        normalCollider =  GetComponent<Transform>().GetChild(2);
        dashCollider = GetComponent<Transform>().GetChild(3);
        physicsBody = GetComponent<Rigidbody2D>();

        originalConstraints = physicsBody.constraints;
        dashing = false;
        direction = 1;
    }

    private void FixedUpdate(){
        DashAbility();
    }

    public void DashAbility(){
        float dir = Input.GetAxis("Horizontal");

        if(dir < 0){
            direction = -1;
        }else if(dir>0){
            direction = 1;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !dashing && !spacePressed){
            dash = DashAb.Dashing;
            dashing = true;
        }
        switch(dash){
            case DashAb.Ready:
                break;
            case DashAb.Dashing:
                normalCollider.GameObject().SetActive(false);
                dashCollider.GameObject().SetActive(true);
                physicsBody.velocity = new Vector2((dashForce+movespeed)*direction, physicsBody.velocity.y);
                Timer(duration, DashAb.CoolDown);
                break;
            case DashAb.CoolDown:
                normalCollider.GameObject().SetActive(true);
                dashCollider.GameObject().SetActive(false);
                physicsBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                physicsBody.constraints = originalConstraints;
                dash = DashAb.End;
                break;
            case DashAb.End:
                if(Timer(coolDown,  DashAb.Ready)) dashing = false;
                break;
        }
    }

    private bool Timer(float delay, DashAb dash){
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

    public bool Dashstate(){
        return dash != DashAb.Dashing;
    }

    public void DashTime(float lowerCD){
        if(coolDown>=0.3){
            coolDown-=lowerCD;
        }
    }
    
}
