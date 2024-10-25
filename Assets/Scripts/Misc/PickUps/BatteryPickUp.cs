using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    [SerializeField] private float DashCoolDownReduction;

    private void OnTriggerEnter2D(Collider2D player) {
        if(player.CompareTag("Player")){
            DoStuff(player);
            Destroy(gameObject);
        }
    }

    private void DoStuff(Collider2D player = null){
        switch (this.name){
            case "Bullet":
                break;

            case "Heart":
            //Heal(x, y)   ~~> x is healing health, and the y is healing shield
                player.GetComponentInParent<Health>().Heal(99999, 0);          
                break;

            case "Health":
                player.GetComponentInParent<Health>().Heal(1,0);
                break;

            case "Shield":
                player.GetComponentInParent<Health>().Heal(0, 99999, 2);
                break;

            case "Battery":
                player.GetComponentInParent<Moveme>().DashTime(DashCoolDownReduction);
                break;
        }
    }

}
