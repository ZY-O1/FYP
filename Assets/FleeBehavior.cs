using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    public float EnemyDistance = 31.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 10;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(animator.transform.position, player.transform.position);
        Vector3 dirToPlayer = animator.transform.position - player.transform.position;
        Vector3 newPos = animator.transform.position + dirToPlayer;
        this.agent.SetDestination(newPos);

        if (distance > EnemyDistance)
        {
            animator.SetBool("isFleeing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 3;
    }
}
