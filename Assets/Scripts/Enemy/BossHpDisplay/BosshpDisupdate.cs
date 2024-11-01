using UnityEngine.UI;
using UnityEngine;

public class BosshpDisupdate : MonoBehaviour
{
    public Image healthBar;

    [SerializeField] protected int totalHP;

    [SerializeField] protected int totalHPmodified;

    [SerializeField] protected Health bossHealth;


    private void Awake(){
        if(bossHealth == null){
            bossHealth = GetComponent<Health>();
            totalHP = bossHealth.GetHealth();
            totalHPmodified = totalHP;
        }
    }

    public void TakeDamage(int damage){
        totalHPmodified-=damage;
        healthBar.fillAmount = (float)totalHPmodified/(float)totalHP;
    }
}
