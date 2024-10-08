using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float health;

    public float GetHealth() {
        return health;
    }

    public void UpdateHealth(float dmg) {
        health -= dmg;
    }
}
