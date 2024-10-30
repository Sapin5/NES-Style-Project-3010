using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossWin : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private GameObject LossScreen;
    private void Awake(){
        if(health == null){
            health = FindAnyObjectByType<PlayerHealth>();
        }
    }

    private void Update(){
        if(health.RemainingHealth() <= 0){
            LossScreen.SetActive(true);
        }
    }
}
