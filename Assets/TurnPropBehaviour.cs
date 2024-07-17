using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPropBehaviour : StateMachineBehaviour
{
    Transform player;
    SwapTeam st;
    StaminaAndEnergy sae;
    Enemy e;

    public bool move;

    void OnBecameVisible()
    {
        move = false;
    }

    void OnBecameInvisible()
    {
        move = true;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        e = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        if (move)
        {

        }
        else if (!move)
        {
            e.propNumber();
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        st = GameObject.FindGameObjectWithTag("Player").GetComponent<SwapTeam>();
        sae = GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaAndEnergy>();

        //animator.transform.LookAt(player);
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < 20 && move)
            animator.SetBool("isFleeing", true);
        else if (distance > 20 && !move)
            animator.SetBool("isSafe", false);
        else
            animator.SetBool("isPatrolling", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // animator.SetBool("isPatrolling", true);
    }
}
