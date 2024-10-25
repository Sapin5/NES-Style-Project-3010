using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] [Range(0f, 0.5f)] private float xPadding;
    [SerializeField] private GameObject heartPrefab;

    [Header("Sprites:")]
    [SerializeField] private Sprite FullHP_Sprite;
    [SerializeField] private Sprite HalfHP_Sprite;
    [SerializeField] private Sprite EmptyHP_Sprite;

    //Default values
    private int numHearts = 1;
    private int hp = 2;

    public void SetUpUI(int newHP) {
        hp = newHP;
        numHearts = hp/2;
        UpdateUI();
    }

    public void UpdateHealth(int newHP)
    {
        hp = newHP;
        UpdateUI();
    }

    private void UpdateUI()
    {
        //Clear old UI
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        //Display new UI
        for (int i = 0; i < numHearts; i++)
        {
            Vector3 spaceFromOrigin = new Vector3(i * xPadding, 0, 0);

            GameObject heartObject = Instantiate(heartPrefab, transform.position + spaceFromOrigin, transform.rotation);
            heartObject.transform.SetParent(transform);

            if (hp >= (i+1)*2)
                heartObject.GetComponent<SpriteRenderer>().sprite = FullHP_Sprite;
            else if (hp > i*2 && hp % 2 != 0)
                heartObject.GetComponent<SpriteRenderer>().sprite = HalfHP_Sprite;
            else
                heartObject.GetComponent<SpriteRenderer>().sprite = EmptyHP_Sprite;
        }
    }
}
