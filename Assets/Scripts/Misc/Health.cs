using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Animator animator;

    public int GetHealth() {
        return health;
    }

    private void UpdateHealth(int dmg) {
        health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damage>() != null) {

            Debug.Log(health);
            UpdateHealth(other.GetComponent<Damage>().GetDamage());
            if (health > 0)
                animator.SetTrigger("OnHit");
            else
                animator.SetTrigger("Die");
        }
    }


}
