using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Sprite Properties:")]
    [SerializeField] private SpriteRenderer enemyRenderer; 

    [Header("Movement Properties:")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float xWanderMaxDist;
    [SerializeField] [Range(1f, 10f)] private float maxIdleTime;

    [SerializeField] private Animator enemyAnimator;

    private Vector3 originPos;
    private float currentTimer;
    private float idleTime;

    //Enemy Movement stuff. . .  (Sup santiago, why are u snooping around my code..........................)
    private float endPosX;
    private int direction;

    private void Start()
    {
        originPos = transform.position;
        CreateNewPath();
    }

    private void Update() {
        MovementHandler();
    }

    private void MovementHandler() {
        currentTimer += Time.deltaTime;

        if (currentTimer >= idleTime) {

            if ((direction == 1 && transform.position.x < endPosX) || (direction == -1 && transform.position.x > endPosX)) {
                //Update the position
                transform.Translate(new Vector3(direction * movementSpeed * Time.deltaTime, 0, 0));

                //Flip the enemy sprite depending on direction
                enemyRenderer.flipX = direction == 1 ? false : true;
                enemyAnimator.SetBool("EnemyWalk", true);

            } else {
                CreateNewPath();
            }
        }
    }
    private void CreateNewPath()
    {
        currentTimer = 0;
        idleTime = Random.Range(1f, maxIdleTime);

        endPosX = originPos.x + Random.Range(-xWanderMaxDist, xWanderMaxDist);
        
        direction = endPosX > transform.position.x ? 1 : -1;
        enemyAnimator.SetBool("EnemyWalk", false);
    }    
}
