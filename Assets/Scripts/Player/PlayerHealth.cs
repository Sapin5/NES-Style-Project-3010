using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 10;
    private float health;

    void Update(){
        Debug.Log($"Health is {health}");
    }
    private void Awake(){
        health=totalHealth;
    }

    private void UpdateHealth(float dmg) {
        health -= dmg;
    }

    public void Heal(){
        health += 1;
    }

    public void FullHeal(){
        float tempHealth = totalHealth-health;

        health+=tempHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weapon")){
            UpdateHealth(other.transform.GetComponent<Damage>().GetDamage());
            Debug.Log(health);
        }
    }
    
}
