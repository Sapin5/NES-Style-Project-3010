using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("General Properties:")]
    [SerializeField] private int health;
    [SerializeField] private int shield;

    [Header("Enemy Properties:")]
    [SerializeField] private Animator enemyAnimator;

    [Header("Player Properties:")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private HealthDisplay healthDisplay;
    [SerializeField] private ShieldDisplay shieldDisplay;

    private int originalHealth;
    private int originalShield;

    private void Awake() {
        originalHealth = health;
        originalShield = shield;

        if (healthDisplay == null) {
            healthDisplay = FindObjectOfType<HealthDisplay>();
        }
        if (shieldDisplay == null) {
            shieldDisplay = FindObjectOfType<ShieldDisplay>();
        }

        if (health % 2 != 0 && isPlayer) {
            Debug.LogError("THE PLAYER'S HP MUST BE EVEN !!! >:( ");
            health += 1;
        } else if (shield % 2 != 0 && isPlayer) {
            Debug.LogError("THE PLAYER'S SHIELD MUST BE EVEN !!! >:( ");
            health += 1;
        }

        if (isPlayer) {
            healthDisplay.SetUpUI(health);
            shieldDisplay.SetUpUI(health);
        }
    }

    private void Update() {
    }

    public int GetHealth() {
        return health;
    }

    public int GetShield() {
        return health;
    }

    private void IntakeDamage(int dmg) {

        if (isPlayer && shield > 0) {
            shield -= dmg; 
            dmg = shield < 0 ? -shield : 0;
            shieldDisplay.UpdateShield(shield);
        }
        
        health -= dmg;
        
        if (isPlayer && shield <= 0)
            healthDisplay.UpdateHealth(health);
    }

    public void Heal(int healHealth, int healShield, int extraShield = 0) {
        if(extraShield%2!=0){
            extraShield+=1;
        }
        if (isPlayer) {
            health += healHealth;
            shield += healShield;

            health = health > originalHealth ? originalHealth : health;
            shield = shield > originalShield ? originalShield: shield;

            healthDisplay.UpdateHealth(health);
            shieldDisplay.UpdateShield(shield + extraShield);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damage>() != null) {
            IntakeDamage(other.GetComponent<Damage>().GetDamage());
            if (health > 0 && !isPlayer)
                enemyAnimator.SetTrigger("OnHit");
            else if (health <= 0 && !isPlayer)
                enemyAnimator.SetTrigger("Die");
        }
    }


}
