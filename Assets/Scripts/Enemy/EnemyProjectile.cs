using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float speed = 0f;
    private Vector3 direction = new Vector3(0,0,0);

    public void SetProjectile(float speed, Vector3 direction) {
        this.speed = speed;
        this.direction = direction;
    }

    private void Update() {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
}
