using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerGUIHandler : MonoBehaviour
{
    [Header("Health GUI Properties:")]
    [SerializeField] bool isHealthGUIHandler;
    [SerializeField] Sprite fullHP;
    [SerializeField] Sprite emptyHP;
    private List<GameObject> healthGUIObjects = new List<GameObject>();


    [Header("Shield GUI Properties:")]
    [SerializeField] bool isShieldGUIHandler;
    [SerializeField] Sprite fullShield;
    [SerializeField] Sprite emptyShield;
    private List<GameObject> shieldGUIObjects = new List<GameObject>();



    private void Awake() {
        if (isHealthGUIHandler) {
            for (int i = 0; i < transform.childCount; i++) {
                healthGUIObjects.Add(transform.GetChild(i).gameObject);
            } 
        } else if (isShieldGUIHandler) {
            for (int i = 0; i < transform.childCount; i++) {
                shieldGUIObjects.Add(transform.GetChild(i).gameObject);
            } 
        }
        
    }

    public void UpdateHealthGUI(int newHealth) {
        for (int i = 0; i < healthGUIObjects.Count; i++) {
            if (i < newHealth)
                healthGUIObjects[i].GetComponent<Image>().sprite = fullHP;
            else
                healthGUIObjects[i].GetComponent<Image>().sprite = emptyHP;
        }
    }

    public void UpdateShieldGUI(int newShield) {
        for (int i = 0; i < shieldGUIObjects.Count; i++) {
            if (i < newShield)
                shieldGUIObjects[i].GetComponent<Image>().sprite = fullHP;
            else
                shieldGUIObjects[i].GetComponent<Image>().sprite = emptyHP;
        }
    }
}
