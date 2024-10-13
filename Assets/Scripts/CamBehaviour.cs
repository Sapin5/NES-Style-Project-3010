using UnityEngine;

public class CamBehaviour : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;

    public float smoothSpeed = 0.125f;

    private void Update() {
        if (player != null)
        {
            // Target position for the camera
            Vector3 targetPosition = player.position + offset;

            // Smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;
        }
    }
}
