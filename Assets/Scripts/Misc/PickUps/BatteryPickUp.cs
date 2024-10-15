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
                player.GetComponentInParent<PlayerHealth>().FullHeal();
                break;

            case "Health":
                player.GetComponentInParent<PlayerHealth>().Heal();
                break;

            case "Shield":
                break;

            case "Battery":
                player.GetComponentInParent<Moveme>().DashTime(DashCoolDownReduction);
                break;
        }
    }

}