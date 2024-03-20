using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;

    private Transform player;
    private Animator animator;
    private float nextAttackTime;

    public float AttackDamage
    {
        get { return attackDamage; }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                Attack();
            }
            else
            {
                animator.SetBool("IsWalking", true);
            }
        }
    }

    public void Attack()
    {
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("Attack");
        nextAttackTime = Time.time + attackCooldown;
    }

    // Add a public method to check if the zombie is currently attacking
    public bool IsAttacking()
    {
        // Check if the zombie is in the attacking animation state
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }
}
