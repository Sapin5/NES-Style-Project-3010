using UnityEngine;


public class Health : MonoBehaviour
{
    [Header("Health Properties:")]
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    
    [Header("Audio Properties:")]
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    [Header("Select if it is boss:")]
    [SerializeField] private bool isBoss;
    [SerializeField] protected BosshpDisupdate bossHealth;

    private const float audioVolume = 0.2f;

    void Awake(){
        if(isBoss){
            if(bossHealth == null) bossHealth = GetComponent<BosshpDisupdate>();
        }
    }

    private void OnEnable() {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public int GetHealth() {
        return health;
    }

    private void UpdateHealth(int dmg) {
        bossHealth.TakeDamage(dmg);
        health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damage>() != null) {
            UpdateHealth(other.GetComponent<Damage>().GetDamage());
            if (health > 0) {
                animator.SetTrigger("OnHit");
                if (audioClips[1] != null) audioSource.PlayOneShot(audioClips[1], audioVolume); //Play onhit sound
            } else {
                animator.SetTrigger("Die");
                if (audioClips[0] != null) audioSource.PlayOneShot(audioClips[0], audioVolume); //Play death sound
            }
        }
    }
}
