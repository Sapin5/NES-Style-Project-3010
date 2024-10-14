using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Transform wielderTransform;
    [SerializeField] private float damage;
    [SerializeField] private float knockbackStrength;
    [SerializeField] [Range(0.1f, 2f)] private float knockbackDebounce;

    private float currentTimer = 0;

    void Awake(){
        if(wielderTransform == null){
            wielderTransform = GetComponent<Transform>();
        }
    }

    public float GetDamage() {
        return damage;
    }

    private void Update() {
        currentTimer += Time.deltaTime;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        ApplyKnockback(other);
    }

    private void ApplyKnockback(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>() != null && currentTimer >= knockbackDebounce)
        {
            Vector2 forceDirection = new Vector2((other.transform.position.x - wielderTransform.position.x) > 0 ? 1 : -1, 0);

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * knockbackStrength, ForceMode2D.Impulse);

            currentTimer = 0;
        }
    }

}
