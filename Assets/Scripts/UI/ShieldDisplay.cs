using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDisplay : HPDisplay
{
    void Awake(){
        IncreaseShields();
    }
    public void IncreaseShields(){
       // Instantiate(GameObject(Shield), transform.localPosition, new Quaternion(0f,0f,0f,0f)); 
    }
}
