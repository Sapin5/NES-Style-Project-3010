using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyStateHandler : MonoBehaviour
{
    [Header("Enemy State Properties:")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector2 chaseRange;
    [SerializeField] private Vector2 attackRange;

    [SerializeField] private EnemyChase chasingState;
    [SerializeField] private EnemyPatrol patrollingState;
    [SerializeField] private EnemyAttack attackingState;

    private void Update() {
        Vector2 distFromPlayer = playerPos.position - transform.position;
        distFromPlayer.x = Math.Abs(distFromPlayer.x);
        distFromPlayer.y = Math.Abs(distFromPlayer.y); 

        if ((distFromPlayer.x <= attackRange.x) && (distFromPlayer.y <= attackRange.y)) 
        {
            EnterAttackingState();
        } 
        else if ((distFromPlayer.x <= chaseRange.x) && (distFromPlayer.y <= chaseRange.y) && !attackingState.isAttacking)
        {
            EnterChasingState();
        }
        else if (((distFromPlayer.x > chaseRange.x) || (distFromPlayer.y > chaseRange.y)) && !attackingState.isAttacking) {
            EnterPatrollingState();
        }
    }

    private void EnterPatrollingState() {
        Debug.Log("Enemy is patrolling.");
        
        patrollingState.enabled = true;
        
        chasingState.enabled = false;
        attackingState.enabled = false;
    }

    private void EnterChasingState() {
        Debug.Log("Enemy is chasing the player.");

        chasingState.enabled = true;

        attackingState.enabled = false;
        patrollingState.enabled = false;
    }

    private void EnterAttackingState() {
        Debug.Log("Enemy is now attacking!");

        attackingState.enabled = true;

        chasingState.enabled = false;
        patrollingState.enabled = false;
    }
}
