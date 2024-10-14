using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class Platform : MonoBehaviour
{
    private Rigidbody2D playerBody;
    private PlatformEffector2D platformEffector;
    
    [SerializeField] private LayerMask excludePlayerMask;
    [SerializeField] private LayerMask includePlayerMask;
    [SerializeField] private float surfaceArc = 178f;
    [SerializeField] private float dropDownTimer;

    private float currentTimer;

    private void Awake() {
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        platformEffector = GetComponent<PlatformEffector2D>();
        platformEffector.surfaceArc = surfaceArc;

        currentTimer = dropDownTimer;
    }

    private void Update() {
        bool registerDownInput = Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.S);
        currentTimer += Time.deltaTime;

        if (playerBody != null && registerDownInput && playerBody.velocity.y == 0f) {
            platformEffector.colliderMask = excludePlayerMask;
            currentTimer = 0f;

        } else if (currentTimer >= dropDownTimer) {
            platformEffector.colliderMask = includePlayerMask;
        }
    }
}
