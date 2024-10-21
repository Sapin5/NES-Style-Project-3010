using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header("Shield Properties:")]
    [SerializeField] private float totalShield = 10;
    private float shield;
    [SerializeField] ShieldDisplay shieldDisplay;
    private float timer = 0;

    private void Awake(){
        if(totalShield%2!=0){
            totalShield+=1;
        }
        shield = totalShield;
        shieldDisplay = FindAnyObjectByType<Canvas>().GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<ShieldDisplay>();
    }

    void Update(){
        Debug.Log($"shield is {shield}");
        if(shield!=totalShield && Timer(4)){
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
        if(shield > 0){
            shield -= dmg;
            shieldDisplay.UpdateHP();
        }
    }

    public void IncreaseShield(int amount){
        shieldDisplay.IncreaseShields();
        totalShield+=amount;
        FullShield();
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

    public float GetShield(){
        return totalShield;
    }
}
