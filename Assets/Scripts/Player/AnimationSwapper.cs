using UnityEngine;

public class AnimationSwapper : MonoBehaviour
{
    private Moveme moveme;
    private Animator animator;
    private PlayerHealth health;

    void Start(){
        moveme = gameObject.GetComponentInParent<Moveme>();
        animator = GetComponent<Animator>();
        health = GetComponentInParent<PlayerHealth>();
    }

    void Update(){
        if(health.RemainingHealth()==0){
            animator.SetTrigger("Dead");
        }else{
            AnimatePlayer();
        }
    }
 
    private void AnimatePlayer(){
        switch (moveme.Action()){
            case "Idle":
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       true);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    false);
            break;

            case "Jumping":
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    true);
                animator.SetBool("Crouching",    false);
            break;

            case "Walking":
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    true);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    false);
            break;

            case "Dashing":
                animator.SetBool("Dashing",    true);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    false);
            break;

            case "Crouching":
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    true);
                break;
        }
    }
}