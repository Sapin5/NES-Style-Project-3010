using UnityEngine;

public class BatteryPickUp : MonoBehaviour
{
    [SerializeField] private float DashCoolDownReduction;

    private void OnTriggerEnter2D(Collider2D player) {
        if(player.CompareTag("Player")){

            switch (this.name){
                case "Bullet":
                    break;

                case "Heart":
                    player.GetComponentInParent<Moveme>().DashTime(DashCoolDownReduction);
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
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D player) {
        if(player.transform.CompareTag("Player")){
            player.transform.GetComponentInParent<Moveme>().DashTime(DashCoolDownReduction);
            Destroy(gameObject);
        }
    }
}
