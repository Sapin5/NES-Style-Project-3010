using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject rightWeapon;
    [SerializeField] private GameObject leftWeapon;
    
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
        
        if (xDirection == 1) {
            enemyRenderer.flipX = false;
        } else {
            enemyRenderer.flipX = true;
        }
    }

    public void ActivateHitBox() {
        if (xDirection == 1) {
            leftWeapon.SetActive(false);
            rightWeapon.SetActive(true);
        } else {
            leftWeapon.SetActive(true);
            rightWeapon.SetActive(false);
        }
    }

    public void DisableHitBox() {
        leftWeapon.SetActive(false);
        rightWeapon.SetActive(false);
    }
}
