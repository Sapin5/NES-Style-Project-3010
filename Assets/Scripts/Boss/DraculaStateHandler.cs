using UnityEngine;
using UnityEngine.UIElements;

public class DraculaStateHandler : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField] private float maxIdleTime;
    [SerializeField] private Animator draculaAnimator;
    
    [Header("Audio Settings:")]
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;
    private const float AUDIO_VOLUME = 0.2f;


    [Header("Dracula Melee Attack 2 Properties:")]
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    private Transform playerPos;
    private bool isAttacking2 = false;
    private Vector3 movePosition2;
    
    private enum State { Idle, MeleeAttack1 , MeleeAttack2 , ProjectileAttack } ;
    private State currentState;

    private float currentIdleTimer = 0f;

    private Vector3 ORIGINAL_POS = new Vector3(0.65f, 22f, 0); 

    private void Awake()
    {
        currentState = State.Idle;
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (playerPos == null) {
            Debug.LogError("No player position in DraculaStateHandler class. D: ");
        }
    }

    private void OnEnable() {
        audioSource = GetComponentInChildren<AudioSource>();
    }


    private void Update()
    {
        if (!isActive) return;

        switch (currentState) {

            case State.Idle: //---------------------------IDLE---------------------------------
                draculaAnimator.SetBool("Idle", true);

                draculaAnimator.SetBool("MeleeAttack1", false);
                draculaAnimator.SetBool("MeleeAttack2", false);
                draculaAnimator.SetBool("ProjectileAttack", false);

                currentIdleTimer += Time.deltaTime;

                if (currentIdleTimer >= maxIdleTime) {
                    ChooseRandomState();
                    currentIdleTimer = 0;
                }

                return;

            case State.MeleeAttack1: //---------------------------MELEE ATTACK 1---------------------------------
                draculaAnimator.SetBool("MeleeAttack1", true);

                draculaAnimator.SetBool("Idle", false);
                draculaAnimator.SetBool("MeleeAttack2", false);
                draculaAnimator.SetBool("ProjectileAttack", false);
                
                return;

            case State.MeleeAttack2: //---------------------------MELEE ATTACK 2---------------------------------
                draculaAnimator.SetBool("MeleeAttack2", true);

                draculaAnimator.SetBool("Idle", false);
                draculaAnimator.SetBool("MeleeAttack1", false);
                draculaAnimator.SetBool("ProjectileAttack", false);

                return;

            case State.ProjectileAttack: //---------------------------PROJECTILE ATTACK---------------------------------
                draculaAnimator.SetBool("ProjectileAttack", true);

                draculaAnimator.SetBool("MeleeAttack2", false);
                draculaAnimator.SetBool("Idle", false);
                draculaAnimator.SetBool("MeleeAttack1", false);

                return;
        }
    
    }

    //Choose a random state that isn't the current state
    public void ChooseRandomState() { 
        const int numStates = 400;

        bool foundState = false;
        State calculatedState = State.Idle;

        while (!foundState) {
            int randomState = Random.Range(0, numStates);

            if (randomState < 100) {
                calculatedState = State.Idle;
            } else if (randomState < 200) {
                calculatedState = State.MeleeAttack1;
            } else if (randomState < 300) {
                calculatedState = State.MeleeAttack2;
            } else {
                calculatedState = State.ProjectileAttack;
            }

            if (currentState != calculatedState) {
                foundState = true;
            }
        }

        currentState = calculatedState;
    }
    

    //Used for the melee attack 2, for repositioning infront of player
    public void MoveBesidePlayer() {
        movePosition2 = playerPos.position + new Vector3(xOffset, yOffset, 0);
        isAttacking2 = true;
    }

    //Used for the melee attack 2, for repositioning back to the original position
    public void MoveToOriginalPos() {
        isAttacking2 = false;
    }

    //For moving when casting melee attack 2
    private void LateUpdate() {
        if (currentState == State.MeleeAttack2 && isAttacking2) {
            transform.position = movePosition2;
        } else if (currentState == State.MeleeAttack2 && !isAttacking2) {
            transform.position = ORIGINAL_POS;
        }
    }

    public void PlayMelee1Sound() {
        audioSource.PlayOneShot(audioClips[0], AUDIO_VOLUME);
    }

    public void PlayMelee2Sound() {
        audioSource.PlayOneShot(audioClips[1], AUDIO_VOLUME);
    }

    public void PlayRangeSound() {
        audioSource.PlayOneShot(audioClips[2], AUDIO_VOLUME);
    }
}
