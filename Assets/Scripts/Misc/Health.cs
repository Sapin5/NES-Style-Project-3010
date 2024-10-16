using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private int totalHealth;
    private int health;
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
            UpdateHealth(other.GetComponent<Damage>().GetDamage());
            Debug.Log(other.transform.name);
            if (health > 0)
                animator.SetTrigger("OnHit");
            else
                animator.SetTrigger("Die");
        }
    }


}
