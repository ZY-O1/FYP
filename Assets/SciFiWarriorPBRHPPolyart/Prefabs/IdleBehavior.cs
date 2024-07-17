using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    float timer;
    Transform player;
    float chaseRange = 30f;
    SwapTeam st;
    StaminaAndEnergy sae;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        st = GameObject.FindGameObjectWithTag("Player").GetComponent<SwapTeam>();
        sae = GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaAndEnergy>();

        timer += Time.deltaTime;

        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (timer > 5 && distance > 15)
            animator.SetBool("isPatrolling", true);

        if (distance < 10 && st.propNum == 0 && SwapTeam.team == 1|| sae.leak && SwapTeam.team == 1 || TIMER.currentMatchTimer < 180)
            animator.SetBool("isChasing", true);
        else if (distance < chaseRange && SwapTeam.team == 2)
            animator.SetBool("isFleeing", true);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }
}
