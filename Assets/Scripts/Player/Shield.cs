using UnityEngine;
using UnityEngine.SceneManagement;

public class Shield : MonoBehaviour
{
    [SerializeField] private float totalShield = 10;
    private float shield;
    [SerializeField] HPDisplay shieldDisplay;
    private float timer = 0;

    private void Awake(){
        shield=totalShield;
        shield = totalShield;
        shieldDisplay = FindAnyObjectByType<Canvas>().GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<ShieldDisplay>();
    }

    void Update(){
        Debug.Log($"shield is {shield}");
        if(shield!=totalShield && Timer(2)){
            shield += 1;
            shieldDisplay.HealOne();
        }

        if(Input.GetKeyDown(KeyCode.J) && shield !=0){
            Updateshield(1);
        }
    }

    private bool Timer(float delay){
        timer+=Time.deltaTime;
        // Switches when timer becomes greater than the delay
        if(timer>delay){
            // resets timer
            timer=0;
            // Switches firingState to whatver state was passed
            return true;
        }else return false;
    }

    private void Updateshield(float dmg) {
        shield -= dmg;
        shieldDisplay.UpdateHP();
    }

    public void FullShield(){
        float tempshield = totalShield-shield;
        shieldDisplay.FullHeal();
        shield+=tempshield;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weapon")){
            Updateshield(other.transform.GetComponent<Damage>().GetDamage());
            Debug.Log(shield);
        }
    }

    public bool ShieldLeft(){
        if(shield == 0){
            return false;
        }else{
            return true;
        }
    }
}
