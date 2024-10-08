using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] Transform wielderTransform;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackStrength;

    public float GetDamage() {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<Rigidbody2D>() != null) {
            Vector2 force = other.transform.position - wielderTransform.position;

            Debug.Log("BOOM");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(force * knockbackStrength, ForceMode2D.Impulse);
        }
    }
}
