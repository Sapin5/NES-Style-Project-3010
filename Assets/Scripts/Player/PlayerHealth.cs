using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int totalHealth = 10;
    private int health;

    void Update(){
        Debug.Log($"Health is {health}");
    }
    private void Awake(){
        health=totalHealth;
    }

    private void UpdateHealth(int dmg) {
        health -= dmg;
    }

    public void Heal(){
        if(health!=totalHealth)health += 1;
    }

    public void FullHeal(){
        int tempHealth = totalHealth-health;

        health+=tempHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weapon")){
            UpdateHealth(other.transform.GetComponent<Damage>().GetDamage());
            Debug.Log(health);
        }
    }
    
}
