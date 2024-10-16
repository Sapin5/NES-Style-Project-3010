using UnityEngine;


public class EnemyChase : MonoBehaviour
{
    [Header("Chasing Properties:")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private SpriteRenderer enemyRenderer; 

    [Header("Flying Enemy Chasing Properties:")]
    [SerializeField] private bool isFlyingEnemy;
    [SerializeField] private float yFromPlayer;
    [SerializeField] private float withinTargetRangeY;

    private void Awake() {
        if(playerPos == null){
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    private void Update() {
        ChasePlayer();
    }

    private void ChasePlayer() {
        
        //Handles horizontal chasing
        int xDirection = (playerPos.position.x - transform.position.x) > 0 ? 1 : -1;
        enemyRenderer.flipX = xDirection == 1 ? false : true;
        float moveIncrementX = xDirection * chaseSpeed * Time.deltaTime;


        float moveIncrementY = 0f;

        //Handles vertical chasing (if it's a flying enemy)
        if (isFlyingEnemy) {
            moveIncrementY = chaseSpeed * Time.deltaTime;
            if (transform.position.y > playerPos.position.y + yFromPlayer + withinTargetRangeY) {
                moveIncrementY *= -1f;
            } else if (transform.position.y >= playerPos.position.y + yFromPlayer - withinTargetRangeY) {
                moveIncrementY = 0f;
            }            
        }


        transform.Translate(new Vector2(moveIncrementX, moveIncrementY));
    }
}
