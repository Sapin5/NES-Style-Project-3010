using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTracking : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Awake() {
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void Updated(){
        
    }
}
