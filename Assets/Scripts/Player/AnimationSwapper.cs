using UnityEngine;

public class AnimationSwapper : MonoBehaviour
{
    private Moveme moveme;
    private Animator child;
    private PlayerHealth health;

    void Start(){
        moveme = gameObject.GetComponentInParent<Moveme>();
        child = GetComponent<Animator>();
        health = GetComponentInParent<PlayerHealth>();
    }

    void Update(){
        if(health.RemainingHealth()==0){
            child.SetTrigger("Dead");
        }else{
            AnimatePlayer();
        }
    }
 
    private void AnimatePlayer(){
        switch (moveme.Action()){
            case "Idle":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       true);
                child.SetBool("Running",    false);
                child.SetBool("Jumping",    false);
                child.SetBool("Crouching",    false);
            break;

            case "Jumping":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    false);
                child.SetBool("Jumping",    true);
                child.SetBool("Crouching",    false);
            break;

            case "Walking":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    true);
                child.SetBool("Jumping",    false);
                child.SetBool("Crouching",    false);
            break;

            case "Dashing":
                child.SetBool("Dashing",    true);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    false);
                child.SetBool("Jumping",    false);
                child.SetBool("Crouching",    false);
            break;

            case "Crouching":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    false);
                child.SetBool("Jumping",    false);
                child.SetBool("Crouching",    true);
                break;
        }
    }
}