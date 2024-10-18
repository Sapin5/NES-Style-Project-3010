using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] private Transform attackOriginPos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float selfDestructTimer;

    [SerializeField] private SpriteRenderer enemyRenderer;

    private int xDirection = 1;
    private Transform playerPos;

    private void Awake() {
        if(playerPos == null){
            playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    public void ChooseDirection() {
        xDirection = playerPos.position.x - transform.position.x > 0 ? 1 : -1;

        float originPosX = Math.Abs(attackOriginPos.localPosition.x);
        
        if (xDirection == 1) {
            enemyRenderer.flipX = false;
            attackOriginPos.localPosition = new Vector3(originPosX, attackOriginPos.localPosition.y, 0);
            
        } else {
            enemyRenderer.flipX = true;
            attackOriginPos.localPosition = new Vector3(-originPosX, attackOriginPos.localPosition.y, 0);
        }
    }

    public void RangeAttack() {

        GameObject projectile = Instantiate(projectilePrefab, attackOriginPos.position, Quaternion.identity);

        Vector3 direction = (playerPos.position - attackOriginPos.position).normalized;
        projectile.GetComponent<EnemyProjectile>().SetProjectile(projectileSpeed, direction);
        Destroy(projectile, selfDestructTimer);
    }

}
