using UnityEngine.AI;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;
    float attackRange = 6;

    SwapTeam st;
    StaminaAndEnergy sae;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 8;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        st = GameObject.FindGameObjectWithTag("Player").GetComponent<SwapTeam>();
        sae = GameObject.FindGameObjectWithTag("Player").GetComponent<StaminaAndEnergy>();
        this.agent.SetDestination(player.position);

        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < attackRange && st.propNum == 0 && !st.disappear || distance < attackRange && sae.leak)
            animator.SetBool("isAttacking", true);

        if (st.disappear && !sae.leak && distance < 20 && TIMER.currentMatchTimer > 180|| st.propNum != 0 && !sae.leak && TIMER.currentMatchTimer > 180 || TIMER.currentMatchTimer > 180 && distance > 20 && !sae.leak)
            animator.SetBool("isChasing", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPatrolling", true);
        this.agent.SetDestination(agent.transform.position);
        this.agent.speed = 3;
    }
}
