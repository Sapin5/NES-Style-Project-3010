using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Shield Properties:")]
    [SerializeField] private float totalShield = 10;
    private float shield;
    [SerializeField] private ShieldDisplay shieldDisplay;
    [SerializeField] private float rechargeDelay;
    private float timer = 0;
    private PlayerHealth health;
    private void Awake(){
        if(totalShield%2!=0){
            totalShield+=1;
        }else if(totalShield>30){
            totalShield=30;
        }
        shield = totalShield;
        shieldDisplay = FindAnyObjectByType<Canvas>().GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<ShieldDisplay>();
        health = GetComponent<PlayerHealth>();
    }

    void Update(){
        if(shield!=totalShield && Timer(rechargeDelay) && health.RemainingHealth() > 0){
            shield += 1;
            //shieldDisplay.HealOne();
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
        if(shield > 0){
            shield -= dmg;
            //shieldDisplay.UpdateHP();
        }
    }

    public void IncreaseShield(int amount){
        if(totalShield <30){
            //shieldDisplay.IncreaseShields();
            totalShield+=amount;
            FullShield();
        }else{
            FullShield();
        }
    }

    public void FullShield(){
        float tempshield = totalShield-shield;
        //shieldDisplay.FullHeal();
        shield+=tempshield;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Weapon")){
            Updateshield(other.transform.GetComponent<Damage>().GetDamage());
        }
    }

    public bool ShieldLeft(){
        if(shield == 0){
            return false;
        }else{
            return true;
        }
    }

    public float GetShield(){
        return totalShield;
    }
}
