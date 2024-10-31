using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private bool isTheBossPortal;
    [SerializeField] private DraculaStateHandler draculaState;

    private void Awake() {
        if (draculaState == null) {
            draculaState = FindObjectOfType<DraculaStateHandler>();
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Player")){
            collision.transform.position = target.transform.position;
            if (isTheBossPortal) {
                draculaState.isActive = true;
            }
        }
    }
}
