using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float ZombieHealthPoints = 100f;
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
            if(ZombieHealthPoints<=0){
                Die();
            }

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

    // Bullet detector
    void OnTriggerEnter(Collider other){

        BulletController bullet = other.GetComponentInParent<BulletController>();

        if (bullet != null && other.CompareTag("Bullet")){
            {
                Debug.Log("Zombie hit");
                //TODO : Create bulletDamage attribute
                TakeDamage(25f);
                Destroy(other.gameObject);
            }
        }
    }

    void TakeDamage(float bulletDamage)
    {
        ZombieHealthPoints -= bulletDamage;
        Debug.Log("Zombie took " + bulletDamage + "damage. Current health: " + ZombieHealthPoints);
        
        if (ZombieHealthPoints <= 0){
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Zombie died");
        //Remove gameobject
        Destroy(gameObject);

    }

    
}
