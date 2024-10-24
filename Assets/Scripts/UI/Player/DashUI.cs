using System.Threading;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool dashing;
    private Animator animator;

    private void Awake(){
        animator = GetComponentInChildren<Animator>();

        if (player == null) {
            GameObject[] playerParts = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject part in playerParts) {
                if (part.GetComponent<Moveme>() != null) {
                    player = part;
                    break;
                }
            }
        }
    }

    void Update(){
        dashing = player.GetComponent<Moveme>().Dashing();
        if(dashing){
            animator.SetBool("DashCooldownDone",    false);
        }else{
            animator.SetBool("DashCooldownDone",    true);
        }
    }


}
