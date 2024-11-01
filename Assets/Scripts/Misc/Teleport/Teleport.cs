using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject target;
    [SerializeField] private bool isTheBossPortal;
    [SerializeField] private DraculaStateHandler draculaState;
    public AudioSource audioSource;
    public AudioClip[] clip;
    [SerializeField] private GameObject canvasHpDisplay;
    private void Awake() {
        if (isTheBossPortal) canvasHpDisplay.SetActive(false);
        if (draculaState == null) {
            draculaState = FindObjectOfType<DraculaStateHandler>();
        }
    }
   
    protected void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("Player")){
            collision.transform.position = target.transform.position;
            if (isTheBossPortal) {
                canvasHpDisplay.SetActive(true);
                audioSource.Stop();
                draculaState.isActive = true;
                audioSource.PlayOneShot(clip[1], 1f);
            }
        }
    }
}
