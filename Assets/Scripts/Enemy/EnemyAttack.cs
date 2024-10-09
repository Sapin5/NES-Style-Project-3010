using UnityEngine;
using System.Collections;
using UnityEditor.Animations;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject rightWeapon;
    [SerializeField] private GameObject leftWeapon;
    [SerializeField] [Range(0.1f, 3f)] private float attackDuration;
    [SerializeField] [Range(0.5f, 15f)]private float attackSpeed;
    [SerializeField] private SpriteRenderer enemyRenderer; 
    [SerializeField] private Animator animController;

    
    public bool isAttacking = false;
    private float currentTimer;
    private const float MAX_TIME = 5f;
    private int xDirection = 1;

    private void Update() {
        currentTimer += Time.deltaTime * attackSpeed;
        
        if (!isAttacking) {
            xDirection = (playerPos.position.x - transform.position.x) > 0 ? 1 : -1;
            enemyRenderer.flipX = xDirection == 1 ? false : true;
        }

        if (currentTimer >= MAX_TIME)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        Attack();

        yield return new WaitForSeconds(attackDuration);

        AttackExit();
    }

    private void Attack() {
        if (xDirection == 1) {
            rightWeapon.SetActive(true);
            leftWeapon.SetActive(false);
        } else {
            rightWeapon.SetActive(false);
            leftWeapon.SetActive(true);
        }
        isAttacking = true;
        animController.SetBool("EnemyAttack", true);
    }
    
    private void AttackExit()
    {
        leftWeapon.SetActive(false);
        rightWeapon.SetActive(false);
        currentTimer = 0;
        isAttacking = false;
        animController.SetBool("EnemyAttack", false);
    }
}
