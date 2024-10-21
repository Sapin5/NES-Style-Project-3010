using UnityEngine;

public class DraculaStateHandler : MonoBehaviour
{
    [SerializeField] private float maxIdleTime;
    [SerializeField] private Animator draculaAnimator;
    
    private enum State { Idle, MeleeAttack1 , MeleeAttack2 , ProjectileAttack } ;
    private State currentState;

    private float currentTimer = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        currentState = State.Idle;
        //Pick and play a random state  
    } 

    // Update is called once per frame
    private void Update()
    {
        //Pick a random state

        //if [ATTACK], enable attack animator and turn other states off
        //after attack, move back to original flying spot.
        //once in original spot, idle for ____ amount of seconds.

        // THEN, PICK A RANDOM ATTACK!! 
        //repeat
        //(also if Dracula is attacked, go back to original spot like as explained above, EXCEPT that Dracula is invulnerable)
        //-----------------------------------------------------------
        switch (currentState) {
            case State.Idle:
                draculaAnimator.SetBool("Idle", true);

                if (currentTimer >= maxIdleTime) {
                    currentState = ChooseRandomState();
                }

                return;
            case State.MeleeAttack1:
                draculaAnimator.SetBool("MeleeAttack1", true);
                return;
            case State.MeleeAttack2:
                draculaAnimator.SetBool("MeleeAttack2", true);
                return;
            case State.ProjectileAttack:
                draculaAnimator.SetBool("ProjectileAttack", true);
                return;
        }
    
    }

    //Choose a random state that isn't the current state
    private State ChooseRandomState() { 
        const int numStates = 4;

        bool foundState = false;
        State calculatedState = State.Idle;

        while (!foundState) {
            int randomState = Random.Range(0, numStates);

            switch (randomState) {
                case 0:
                    calculatedState = State.Idle;
                    break;
                case 1:
                    calculatedState = State.MeleeAttack2;
                    break;
                case 2:
                    calculatedState = State.ProjectileAttack;
                    break;
                default:
                    calculatedState = State.Idle;
                    break;
            }

            if (currentState != calculatedState) {
                foundState = true;
            }
        }

        return calculatedState;
    }
    
}
