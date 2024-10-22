using UnityEngine;

public class DraculaStateHandler : MonoBehaviour
{
    [SerializeField] private float maxIdleTime;
    [SerializeField] private Animator draculaAnimator;
    
    private enum State { Idle, MeleeAttack1 , MeleeAttack2 , ProjectileAttack } ;
    private State currentState;

    private float currentIdleTimer = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        currentState = State.Idle;
    } 

    private void Update()
    {
        switch (currentState) {

            case State.Idle: //---------------------------IDLE---------------------------------
                draculaAnimator.SetBool("Idle", true);

                draculaAnimator.SetBool("MeleeAttack1", false);
                draculaAnimator.SetBool("MeleeAttack2", false);
                draculaAnimator.SetBool("ProjectileAttack", false);

                currentIdleTimer += Time.deltaTime;

                if (currentIdleTimer >= maxIdleTime) {
                    ChooseRandomState();
                    currentIdleTimer = 0;
                }

                return;

            case State.MeleeAttack1: //---------------------------MELEE ATTACK 1---------------------------------
                draculaAnimator.SetBool("MeleeAttack1", true);

                draculaAnimator.SetBool("Idle", false);
                draculaAnimator.SetBool("MeleeAttack2", false);
                draculaAnimator.SetBool("ProjectileAttack", false);
                
                return;

            case State.MeleeAttack2: //---------------------------MELEE ATTACK 2---------------------------------
                draculaAnimator.SetBool("MeleeAttack2", true);

                draculaAnimator.SetBool("Idle", false);
                draculaAnimator.SetBool("MeleeAttack1", false);
                draculaAnimator.SetBool("ProjectileAttack", false);

                return;

            case State.ProjectileAttack: //---------------------------PROJECTILE ATTACK---------------------------------
                draculaAnimator.SetBool("ProjectileAttack", true);

                draculaAnimator.SetBool("MeleeAttack2", false);
                draculaAnimator.SetBool("Idle", false);
                draculaAnimator.SetBool("MeleeAttack1", false);

                return;
        }
    
    }

    //Choose a random state that isn't the current state
    public void ChooseRandomState() { 
        const int numStates = 400;

        bool foundState = false;
        State calculatedState = State.Idle;

        while (!foundState) {
            int randomState = Random.Range(0, numStates);

            if (randomState < 100) {
                calculatedState = State.Idle;
            } else if (randomState < 200) {
                calculatedState = State.MeleeAttack1;
            } else if (randomState < 300) {
                calculatedState = State.MeleeAttack2;
            } else {
                calculatedState = State.ProjectileAttack;
            }

            if (currentState != calculatedState) {
                foundState = true;
            }
        }

        currentState = calculatedState;
    }
    
}
