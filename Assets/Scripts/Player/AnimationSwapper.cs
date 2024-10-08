using Unity.VisualScripting;
using UnityEngine;

public class AnimationSwapper : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    private Moveme moveme;
    private Animator child;

    void Start(){
        moveme = gameObject.GetComponentInParent<Moveme>();

        child = GetComponent<Animator>();
    }

    void FixedUpdate(){
        if(isPlayer){
            AnimatePlayer();
        }else{

        }
    }

    private void AnimatePlayer(){
        switch (moveme.Action()){
            case "Idle":
                child.SetBool("Idle", true);
                child.SetBool("Running", false);
                child.SetBool("Attacking", false);
            break;

            case "Jumping":
               
            break;

            case "Attacking":
                child.SetBool("Attacking", true);
                child.SetBool("Idle", false);
            break;

            case "Walking":
                child.SetBool("Running", true);
                child.SetBool("Idle", false);
                child.SetBool("Attacking", false);
            break;

            case "Dashing":

            break;
        }
    }
}
/*
    SetFloat(string name, float value): Sets a float parameter.
    SetInteger(string name, int value): Sets an integer parameter.
    SetBool(string name, bool value): Sets a boolean parameter.
    SetTrigger(string name): Triggers an animation.
*/