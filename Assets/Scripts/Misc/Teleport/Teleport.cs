using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private bool isTheBossPortal;
    [SerializeField] private DraculaStateHandler draculaState;
    public AudioSource audioSource;
    public AudioClip[] clip;

    private void Awake() {
        if (draculaState == null) {
            draculaState = FindObjectOfType<DraculaStateHandler>();
        }
    }
   
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Player")){
            collision.transform.position = target.transform.position;
            if (isTheBossPortal) {
                audioSource.Stop();
                draculaState.isActive = true;
                audioSource.PlayOneShot(clip[1], 0.3f);
            }
        }
    }
}
