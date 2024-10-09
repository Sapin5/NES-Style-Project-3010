using UnityEngine;

public class AnimationSwapper : MonoBehaviour
{
    private Moveme moveme;
    private Animator child;

    void Start(){
        moveme = gameObject.GetComponentInParent<Moveme>();
        child = GetComponent<Animator>();
    }

    void Update(){
        AnimatePlayer();
    }

    private void AnimatePlayer(){
        switch (moveme.Action()){
            case "Idle":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       true);
                child.SetBool("Running",    false);
                child.SetBool("Attacking",  false);
                child.SetBool("Jumping",    false);
            break;

            case "Jumping":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    false);
                child.SetBool("Attacking",  false);
                child.SetBool("Jumping",    true);
            break;

            case "Attacking":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    false);
                child.SetBool("Attacking",  true);
                child.SetBool("Jumping",    false);;
            break;

            case "Walking":
                child.SetBool("Dashing",    false);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    true);
                child.SetBool("Attacking",  false);
                child.SetBool("Jumping",    false);
            break;

            case "Dashing":
                child.SetBool("Dashing",    true);
                child.SetBool("Idle",       false);
                child.SetBool("Running",    false);
                child.SetBool("Attacking",  false);
                child.SetBool("Jumping",    false);
            break;
        }
    }
}