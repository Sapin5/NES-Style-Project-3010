using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Properties:")]
    [SerializeField] private float totalHealth = 10;
    private float health;
    [SerializeField] HPDisplay hpDisplay;
    string currentSceneName;

    [Header("Select Shield if the player has shields:")]
    [SerializeField] private bool shield;

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

        currentSceneName = SceneManager.GetActiveScene().name;
        health=totalHealth;
        hpDisplay = FindAnyObjectByType<Canvas>().GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<HPDisplay>();
    }

    void Update(){
        if(health <= 0){
            SceneManager.LoadScene(currentSceneName);
        }
        Debug.Log($"Health is {health}");
        if(Input.GetKeyDown(KeyCode.J)){
            shield = GetComponent<Shield>().ShieldLeft();
            UpdateHealth(1);
        }
    }
    private void UpdateHealth(float dmg) {
        if(!shield){
            health -= dmg;
            hpDisplay.UpdateHP();
        }

    public void Heal(){
        if(!shield){
            if(health!=totalHealth){
                health += 1;
                hpDisplay.HealOne();
            }
        }
    }

    public void FullHeal(){
        if(!shield){
            float tempHealth = totalHealth-health;
            hpDisplay.FullHeal();
            health+=tempHealth;
        }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weapon")){
            UpdateHealth(other.transform.GetComponent<Damage>().GetDamage());
            shield = GetComponent<Shield>().ShieldLeft();
            Debug.Log(health);
        }
    }

    public float GetHealth(){
        return totalHealth;
    }
}
