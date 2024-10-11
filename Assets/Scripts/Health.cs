using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private Animator animator;

    public float GetHealth() {
        return health;
    }

    private void UpdateHealth(float dmg) {
        health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damage>() != null) {
            UpdateHealth(other.GetComponent<Damage>().GetDamage());
            animator.SetTrigger("OnHit");
        }
    }


}
