using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class moonMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public Animator animator;

    public static bool isJS = false;
    public AudioClip jumpScare;
    bool alreadyJS;
    bool alreadyIdle = false;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    Vector3 distanceToWalkPoint;

    //Attacking
    public float timeBetweenAttacks;
    //bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public string currentState;


    void AnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);

        //reassign state
        currentState = newState;
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !alreadyIdle)
        {
            Idle();
        }
        if (!playerInSightRange && !playerInAttackRange && alreadyIdle)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void Idle()
    {
        //if (!walkPointSet) SearchWalkPoint();
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        animator.Play("Look Around");
        Invoke(nameof(StopIdle), 3f);
    }

    private void Patroling()
    {
        animator.Play("Sneaking");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        Invoke(nameof(ResetIdle), 9f);
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        animator.Play("Sneaking");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        AudioSource audio = GetComponent<AudioSource>();

        isJS = true;

        if (!alreadyJS)
        {
            animator.Play("JS");
            audio.clip = jumpScare;
            audio.PlayOneShot(audio.clip);
            alreadyJS = true;
            Invoke(nameof(ResetJS), 5f); 
        }
            //if (!alreadyAttacked)
            //{
            //    ///Attack code here
            //    Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //    rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //    rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //    ///End of attack code

            //    alreadyAttacked = true;
            //    Invoke(nameof(ResetAttack), timeBetweenAttacks);
            //}
        }

    //private void ResetAttack()
    //{
    //    alreadyAttacked = false;
    //}

    private void ResetJS()
    {
        alreadyJS = false;
    }

    private void ResetIdle()
    {
        alreadyIdle = false;
    }

    private void StopIdle()
    {
        alreadyIdle = true;
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

