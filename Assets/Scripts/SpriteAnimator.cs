using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] [Range(1f, 30f)] private float spriteSpeed;

    private SpriteRenderer spriteRenderer;
    private static float currentTimer;
    private static int currentSprite;

    private const float MAX_TIMER = 10;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentTimer = 0;
        currentSprite = 0;
    }

    private void Update() {
        currentTimer += Time.deltaTime * spriteSpeed;

        if (currentTimer > MAX_TIMER) {
            spriteRenderer.sprite = sprites[++currentSprite % sprites.Length];
            currentTimer = 0;
        }
    }

}
