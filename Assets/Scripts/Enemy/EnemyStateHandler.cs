using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyStateHandler : MonoBehaviour
{
    [Header("Enemy State Properties:")]
    [SerializeField] private Vector2 chaseRange;
    [SerializeField] private Vector2 attackRange;

    [SerializeField] private EnemyChase chasingState;
    [SerializeField] private EnemyPatrol patrollingState;
    [SerializeField] private EnemyAttack attackingState;

    private Animator enemyAnimator;
    private Transform playerPos;

    private void Awake() {
        if(playerPos == null){
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }
    private void Start() {
        enemyAnimator = GetComponent<Animator>();
    }

    private void Update() {
        Debug.Log("Walking?: " + enemyAnimator.GetBool("EnemyWalk") + " , AND attacking?: " + enemyAnimator.GetBool("EnemyAttack"));

        Vector2 distFromPlayer = playerPos.position - transform.position;
        distFromPlayer.x = Math.Abs(distFromPlayer.x);
        distFromPlayer.y = Math.Abs(distFromPlayer.y); 

        if ((distFromPlayer.x <= attackRange.x) && (distFromPlayer.y <= attackRange.y)) 
        {
            EnterAttackingState();
        } 
        else if ((distFromPlayer.x <= chaseRange.x) && (distFromPlayer.y <= chaseRange.y))
        {
            EnterChasingState();
        }
        else if ((distFromPlayer.x > chaseRange.x) || (distFromPlayer.y > chaseRange.y)) {
            EnterPatrollingState();
        }
    }

    private void EnterPatrollingState() {
        Debug.Log("Enemy is patrolling.");
        
        enemyAnimator.SetBool("EnemyAttack", false);
        enemyAnimator.SetBool("EnemyWalk", false);

        if (enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle") {
            patrollingState.enabled = true;
            chasingState.enabled = false;
            attackingState.enabled = false;
        }
        

    }

    private void EnterChasingState() {
        Debug.Log("Enemy is chasing the player.");

        enemyAnimator.SetBool("EnemyWalk", true);
        enemyAnimator.SetBool("EnemyAttack", false);

        if (enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walk") {
            chasingState.enabled = true;
            attackingState.enabled = false;
            patrollingState.enabled = false;
        }
        
    }

    private void EnterAttackingState() {
        Debug.Log("Enemy is now attacking!");

        attackingState.enabled = true;

        chasingState.enabled = false;
        patrollingState.enabled = false;
        
        enemyAnimator.SetBool("EnemyWalk", false);
        enemyAnimator.SetBool("EnemyAttack", true);
    }
}
