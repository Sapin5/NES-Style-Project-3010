using UnityEngine;


public class EnemyChase : MonoBehaviour
{
    [Header("Chasing Properties:")]
    [SerializeField] private float chaseSpeed;
    [SerializeField] private SpriteRenderer enemyRenderer; 

    private Transform playerPos;

    private void Awake() {
        if(playerPos == null){
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    private void Update() {
        ChasePlayer();
    }

    private void ChasePlayer() {
        int xDirection = (playerPos.position.x - transform.position.x) > 0 ? 1 : -1;
        enemyRenderer.flipX = xDirection == 1 ? false : true;

        float moveIncrementX = xDirection * chaseSpeed * Time.deltaTime;
        transform.Translate(new Vector2(moveIncrementX, 0));
    }
}
