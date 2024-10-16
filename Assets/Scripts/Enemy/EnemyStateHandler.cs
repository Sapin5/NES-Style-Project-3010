using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyChase))]
public class EnemyStateHandler : MonoBehaviour
{
    [Header("Enemy State Properties:")]
    [SerializeField] private Vector2 chaseRange;
    [SerializeField] private Vector2 attackRange;

    [SerializeField] private EnemyChase chasingState;
    [SerializeField] private EnemyPatrol patrollingState;
    [SerializeField] private EnemyAttack attackingState;
    [SerializeField] private EnemyRangeAttack rangeAttackingState;

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
        
        enemyAnimator.SetBool("EnemyAttack", false);
        enemyAnimator.SetBool("EnemyWalk", false);

        if (enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle") {
            patrollingState.enabled = true;
            chasingState.enabled = false;

            if (attackingState != null) {
                attackingState.enabled = false;
            } else if (rangeAttackingState != null) {
                rangeAttackingState.enabled = false;
            }
        }
        

    }

    private void EnterChasingState() {

        enemyAnimator.SetBool("EnemyWalk", true);
        enemyAnimator.SetBool("EnemyAttack", false);

        if (enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walk") {
            chasingState.enabled = true;
            patrollingState.enabled = false;

            if (attackingState != null) {
                attackingState.enabled = false;
            } else if (rangeAttackingState != null) {
                rangeAttackingState.enabled = false;
            }
        }
        
    }

    private void EnterAttackingState() {
        
        if (attackingState != null) {
            attackingState.enabled = true;
        } else if (rangeAttackingState != null) {
            rangeAttackingState.enabled = true;
        }

        chasingState.enabled = false;
        patrollingState.enabled = false;
        
        enemyAnimator.SetBool("EnemyWalk", false);
        enemyAnimator.SetBool("EnemyAttack", true);
    }
}
