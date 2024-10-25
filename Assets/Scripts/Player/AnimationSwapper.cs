using System.Diagnostics;
using UnityEngine;

public class AnimationSwapper : MonoBehaviour
{
    private Moveme moveme;
    private Animator animator;
    private PlayerHealth health;
    private ParticleSystem particles;

    void Start(){
        moveme = gameObject.GetComponentInParent<Moveme>();
        animator = GetComponent<Animator>();
        health = GetComponentInParent<PlayerHealth>();
        particles = GetComponent<ParticleSystem>();
    }

    void Update(){
        if(health.RemainingHealth()<=0){
            animator.SetTrigger("Dead");
        }else{
            AnimatePlayer();
        }
    }
 
    private void AnimatePlayer(){
        switch (moveme.Action()){
            case "Idle":
                particles.Stop();
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       true);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    false);
            break;

            case "Jumping":
                particles.Stop();
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    true);
                animator.SetBool("Crouching",    false);
            break;

            case "Walking":
                particles.Stop();
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    true);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    false);
            break;

            case "Dashing":
                particles.Play();
                animator.SetBool("Dashing",    true);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    false);
            break;

            case "Crouching":
                particles.Stop();
                animator.SetBool("Dashing",    false);
                animator.SetBool("Idle",       false);
                animator.SetBool("Running",    false);
                animator.SetBool("Jumping",    false);
                animator.SetBool("Crouching",    true);
                break;
        }
    }

    public void StopParticles(){
        particles.Stop();
    }
}