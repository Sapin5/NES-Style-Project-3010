using System.Runtime.CompilerServices;
using UnityEngine;

public class Act : MonoBehaviour
{
    private Jump jump;
    private Walk walk;
    private Attack attack;
    private Dash dash;

    private void Awake(){
        jump = gameObject.GetComponentInParent<Jump>();
        walk = gameObject.GetComponentInParent<Walk>();
        attack = gameObject.GetComponentInParent<Attack>();
        dash = gameObject.GetComponentInParent<Dash>();
    }

    private void FixedUpdate(){
        walk.Movement();
        jump.Jumping();
        attack.Attacks();
        dash.DashAbility();
    }
}
