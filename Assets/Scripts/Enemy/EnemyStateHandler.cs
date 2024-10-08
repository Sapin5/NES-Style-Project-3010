using System;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyAttack))]
public class EnemyStateHandler : MonoBehaviour
{
    [Header("Enemy State Properties:")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    [SerializeField] private EnemyChase chasingState;
    [SerializeField] private EnemyPatrol patrollingState;
    [SerializeField] private EnemyAttack attackingState;

    private void Update() {
        float distFromPlayerX = Math.Abs(playerPos.position.x - transform.position.x);

        if (distFromPlayerX <= attackRange) 
        {
            EnterAttackingState();
        } 
        else if (distFromPlayerX <= chaseRange)
        {
            EnterChasingState();
        }
        else {
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
