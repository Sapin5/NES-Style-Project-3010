using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float totalHealth = 10;
    private float health;
    [SerializeField] HPDisplay hpDisplay;
    void Update(){
        Debug.Log($"Health is {health}");
        if(Input.GetKeyDown(KeyCode.J)){
            UpdateHealth(1);
        }
    }
    private void Awake(){
        health=totalHealth;
        hpDisplay = FindAnyObjectByType<Canvas>().GetComponentInChildren<HPDisplay>();
    }

    private void UpdateHealth(float dmg) {
        health -= dmg;
        hpDisplay.UpdateHP();
    }

    public void Heal(){
        if(health!=totalHealth){
            health += 1;
            hpDisplay.HealOne();
        }
    }

    public void FullHeal(){
        float tempHealth = totalHealth-health;
        hpDisplay.FullHeal();
        health+=tempHealth;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weapon")){
            UpdateHealth(other.transform.GetComponent<Damage>().GetDamage());
            Debug.Log(health);
        }
    }
}
