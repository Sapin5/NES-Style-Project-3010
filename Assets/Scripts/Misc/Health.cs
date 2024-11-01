using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class Health : MonoBehaviour
{
    [Header("Health Properties:")]
    [SerializeField] private int health;
    [SerializeField] private Animator animator;
    private ScoreHandler scoreHandler;
    
    [Header("Audio Properties:")]
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;


    [Header("Score Properties:")]
    [SerializeField] private int onHitScore = 40;
    [SerializeField] private int deathScore = 100;

    private const float audioVolume = 0.2f;

    private void Awake() {
        if (scoreHandler == null) {
            scoreHandler = FindObjectOfType<ScoreHandler>();
        }
    }

    private void OnEnable() {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public int GetHealth() {
        return health;
    }

    private void UpdateHealth(int dmg) {
        health -= dmg;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Damage>() != null) {
            UpdateHealth(other.GetComponent<Damage>().GetDamage());
            if (health > 0) {
                animator.SetTrigger("OnHit");
                if (audioClips[1] != null) audioSource.PlayOneShot(audioClips[1], audioVolume); //Play onhit sound
                if (!gameObject.CompareTag("Player")) scoreHandler.UpdateScore(onHitScore);
            } else {
                animator.SetTrigger("Die");
                if (audioClips[0] != null) audioSource.PlayOneShot(audioClips[0], audioVolume); //Play death sound
                if (!gameObject.CompareTag("Player")) scoreHandler.UpdateScore(deathScore);
            }
        }
    }
}
