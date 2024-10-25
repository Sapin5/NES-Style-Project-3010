using UnityEngine;

public class ShieldDisplay : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] [Range(0f, 0.5f)] private float xPadding;
    [SerializeField] private GameObject shieldPrefab;

    [Header("Sprites:")]
    [SerializeField] private Sprite FullShield_Sprite;
    [SerializeField] private Sprite HalfShield_Sprite;
    [SerializeField] private Sprite EmptyShield_Sprite;

    //Default values
    private int numShields = 1;
    private int shield = 2;

    public void SetUpUI(int newShield) {
        shield = newShield;
        numShields = shield/2;
        UpdateUI();
    }

    public void UpdateShield(int newShield)
    {
        shield = newShield;
        //CheckIfExtraShield();
        UpdateUI();
    }

    private void UpdateUI()
    {
        numShields = shield/2;
        //Clear old UI
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        //Display new UI
        for (int i = 0; i < numShields; i++)
        {
            Vector3 spaceFromOrigin = new Vector3(i * xPadding, 0, 0);

            GameObject heartObject = Instantiate(shieldPrefab, transform.position + spaceFromOrigin, transform.rotation);
            heartObject.transform.SetParent(transform);

            if (shield >= (i+1)*2)
                heartObject.GetComponent<SpriteRenderer>().sprite = FullShield_Sprite;
            else if (shield > i*2 && shield % 2 != 0)
                heartObject.GetComponent<SpriteRenderer>().sprite = HalfShield_Sprite;
            else
                heartObject.GetComponent<SpriteRenderer>().sprite = EmptyShield_Sprite;
        }
    }
}
