using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] private Transform attackOriginPos;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float selfDestructTimer;

    [SerializeField] private SpriteRenderer enemyRenderer;

    [Header("Variant Flying Enemy:")]
    [SerializeField] private bool isAFlyingVariant;
    [SerializeField] private int numFireballs;
    [SerializeField] private float angleBetween;

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
            if (enemyRenderer)
                enemyRenderer.flipX = false;
            
            attackOriginPos.localPosition = new Vector3(originPosX, attackOriginPos.localPosition.y, 0);
            
        } else {
            if (enemyRenderer)
                enemyRenderer.flipX = true;
            
            attackOriginPos.localPosition = new Vector3(-originPosX, attackOriginPos.localPosition.y, 0);
        }
    }

    public void RangeAttack() {
        if (!isAFlyingVariant) {
            GameObject projectile = Instantiate(projectilePrefab, attackOriginPos.position, Quaternion.identity);

            Vector3 direction = (playerPos.position - attackOriginPos.position).normalized;
            projectile.GetComponent<EnemyProjectile>().SetProjectile(projectileSpeed, direction);
            
            Destroy(projectile, selfDestructTimer);
        } else {
            GameObject[] projectiles = new GameObject[numFireballs];
            for (int i = 0; i < projectiles.Length; i++) {
                projectiles[i] = Instantiate(projectilePrefab, attackOriginPos.position, Quaternion.identity);

                Vector3 direction = (playerPos.position - attackOriginPos.position).normalized;

                direction = Quaternion.Euler(0, 0, i*angleBetween) * direction;
                projectiles[i].GetComponent<EnemyProjectile>().SetProjectile(projectileSpeed, direction);

                Destroy(projectiles[i], selfDestructTimer);
            }

            if (numFireballs % 2 != 0) { //if odd
            }

            Quaternion currentDirection = Quaternion.AngleAxis(angleBetween, Vector3.forward);
            
        }
    }

}
