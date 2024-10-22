using System.Threading;
using UnityEngine;

public class DashUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private bool dashing;
    private Animator animator;
    private void Awake(){
        animator = GetComponentInChildren<Animator>();
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
