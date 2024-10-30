using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Player")){
            collision.transform.position = target.transform.position;
        }
    }
}
