using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damage;

    public float GetDamage() {
        return damage;
    }
}
