using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private float attackDamage = 10f;

    [SerializeField]
    private float attackCooldown = 1f;

    private Transform player;
    private Animator animator; // Add this variable

    private float nextAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        animator = GetComponent<Animator>(); // Get Animator component
        
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            // Calculate the distance between zombie and player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            // If within attack range, trigger attack animation
            Debug.Log("Distance to player: " + distanceToPlayer);
            Debug.Log(agent.stoppingDistance);
            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                Attack();
            }
            else
            {
                animator.SetBool("IsWalking", true); // Start walking animation
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetBool("IsWalking", false); // Stop walking animation
        animator.SetTrigger("Attack"); // Trigger attack animation
        Debug.Log("Attack");

        // Deal damage to player
        //if (player != null)
        //{
        //    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        //    if (playerHealth != null)
        //    {
        //        playerHealth.TakeDamage(attackDamage);
        //    }
        //}

        nextAttackTime = Time.time + attackCooldown;
    }
}