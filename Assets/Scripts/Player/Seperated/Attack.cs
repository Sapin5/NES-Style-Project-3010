using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]private bool spacePressed;
    private float timer;
    private Transform playerDmgBox, playerArt;
    private bool dashState;
    
    void Awake(){
        playerDmgBox = GetComponent<Transform>().GetChild(0);
        playerArt = GetComponent<Transform>().GetChild(1);
        dashState = GetComponent<Dash>().Dashstate();
    }
    public void Attacks(){
        if(Input.GetKey(KeyCode.Space) && !spacePressed && dashState){
            spacePressed = true;
            playerArt.GetComponent<Animator>().SetTrigger("Attack");
        }

        if(spacePressed){
            timer+=Time.deltaTime;
            playerDmgBox.GameObject().SetActive(true);
            if(timer>0.4f){
                playerDmgBox.GameObject().SetActive(false);
                spacePressed = false;
                timer = 0;
           }
        }
    }
}
