using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    [SerializeField]private Transform player;

    [SerializeField]private Vector3 offset = new Vector3(0f, 0.5f, -10f);

    [SerializeField]private float smoothSpeed = 1;

    private Vector3 _velocity;
    
    private void Awake() {
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }

    private void Update() {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity ,smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
