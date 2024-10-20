using UnityEngine;

public class DraculaStateHandler : MonoBehaviour
{
    
    private enum State { Idle, MeleeAttack1 , MeleeAttack2 , ProjectileAttack } ;
    private State currentState;


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
    }

    private State ChooseRandomAttack() {
        const int numAttacks = 3;

        int randomAttack = Random.Range(1, numAttacks + 1);

        switch (randomAttack) {
            case 1:
                return State.MeleeAttack1;
            case 2:
                return State.MeleeAttack2;
            default:
                return State.ProjectileAttack;
        }
    }
}
