using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Properties:")]
    [SerializeField] private float totalHealth = 10;
    private float health;
    [SerializeField] HPDisplay hpDisplay;

    [Header("Select Shield if the player has shields:")]
    [SerializeField] private float shield;

    [SerializeField] private bool hasShield;


    private void Awake(){
        if(totalHealth%2!=0){
            totalHealth+=1;
        }else if(totalHealth > 30){
            totalHealth = 30;
        }

        hasShield = TryGetComponent<Shield>(out _);

        if(hasShield == true){
            shield = GetComponent<Shield>().ShieldLeft();
        }
        health=totalHealth;
        hpDisplay = FindAnyObjectByType<Canvas>().GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<HPDisplay>();
    }

    private void BigHit(int dmg){
        for(int i =0; i<dmg; i ++){
            UpdateHealth(1);
        }
    }

    private void UpdateHealth(float dmg) {
        if(shield == 0){
            Debug.Log(shield);
            health -= dmg;
            hpDisplay.UpdateHP();
        }
    }

    public void Heal(){
        if(shield == 0){
            if(health!=totalHealth){
                health += 1;
                hpDisplay.HealOne();
            }
        }
    }

    public void FullHeal(){
        if(shield == 0){
            float tempHealth = totalHealth-health;
            hpDisplay.FullHeal();
            health+=tempHealth;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        shield = GetComponent<Shield>().ShieldLeft();
        if(other.transform.CompareTag("Weapon") && shield == 0){
            BigHit(other.transform.GetComponent<Damage>().GetDamage());
        }
    }

    public float GetHealth(){
        return totalHealth;
    }

    public float RemainingHealth(){
        return health;
    }
}

