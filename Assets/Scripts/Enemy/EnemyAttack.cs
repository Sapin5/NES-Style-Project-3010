using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject rightWeapon;
    [SerializeField] private GameObject leftWeapon;
    [SerializeField] [Range(0.1f, 10f)] private float attackDuration;
    [SerializeField] [Range(0.5f, 15f)]private float attackSpeed;
    

    private float currentTimer;
    private const float MAX_TIME = 5f;

    private void Update() {
        currentTimer += Time.deltaTime * attackSpeed;

        if (currentTimer >= MAX_TIME) {
            if (currentTimer >= MAX_TIME + attackDuration)
            {
                leftWeapon.SetActive(false);
                rightWeapon.SetActive(false);
                currentTimer = 0;
            } else {
                DecideWeapon();
            }
        }
    }

    private void DecideWeapon() {
        int xDirection = (playerPos.position.x - transform.position.x) > 0 ? 1 : -1;
        if (xDirection == 1) {
            rightWeapon.SetActive(true);
            leftWeapon.SetActive(false);
        } else {
            rightWeapon.SetActive(false);
            leftWeapon.SetActive(true);
        }
    }
}
