using UnityEngine;

[RequireComponent(typeof(EnemyPatrol))]
public class EnemyStateHandler : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    protected enum EnemyState { Patrol, Chase, Attack }
    private static EnemyState currentState;
    

    private void Start() {
        currentState = EnemyState.Patrol;
    }

    private void Update() {
        float distFromPlayer = Vector2.Distance(transform.position, playerPos.position);

        if (distFromPlayer <= attackRange) 
        {
            Debug.Log("Enemy is now attacking!");
            currentState = EnemyState.Attack;
        } 
        else if (distFromPlayer <= chaseRange)
        {
            Debug.Log("Enemy is chasing the player.");
            currentState = EnemyState.Chase;
        }
        else {
            Debug.Log("Enemy is patrolling.");
            currentState = EnemyState.Patrol;
        }
    }

    protected EnemyState GetCurrentState() {
        return currentState;
    }
}
