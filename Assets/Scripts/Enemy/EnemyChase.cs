using UnityEngine;


public class EnemyChase : MonoBehaviour
{
    [Header("Chasing Properties:")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private SpriteRenderer enemyRenderer; 

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
