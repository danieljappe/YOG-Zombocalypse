using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private float ZombieHealthPoints = 100f;

    [SerializeField] private int pointsGiven;
    public ZombieHealthBar zombieHealthBar;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float slowDuration = 0.05f;

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
            zombieHealthBar.SetMaxHealth(ZombieHealthPoints);
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

            // Check if the NavMeshAgent is active before setting destination to avoid getting console errors
            if (agent.isActiveAndEnabled && !hasDied)
            {
                // Your existing code to set destination goes here
                agent.SetDestination(player.position);
            }

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
                TakeDamage(bullet.bulletDamage);
                Destroy(other.gameObject);
            }
        }
    }

    void TakeDamage(float bulletDamage)
{
    ZombieHealthPoints -= bulletDamage;
    zombieHealthBar.SetHealth(ZombieHealthPoints);
    Debug.Log("Zombie took " + bulletDamage + " damage. Current health: " + ZombieHealthPoints);

    if (ZombieHealthPoints <= 0)
    {
        Die();
    }
    else
    {
        StartCoroutine(SlowDown());
    }
}

IEnumerator SlowDown()
{
    // Slow down the zombie's speed
    float originalSpeed = agent.speed;
    agent.speed *= 0.8f; // Adjust the factor to control the degree of slowing down

    yield return new WaitForSeconds(slowDuration);

    // Restore the original speed
    agent.speed = originalSpeed;
}

    
    private bool hasDied = false; // to avoid multiple Instantiates to happen
    void Die()
    {
        if (!hasDied)
        {
            // Add height to spawnPosition to avoid item going through map
            Vector3 spawnPosition = transform.position + Vector3.up * 1f;

            GetComponent<LootBag>().InstantiateLoot(spawnPosition);
            animator.SetTrigger("Death");
            //Disable pathfinding
            agent.enabled = false;
            //Disable ZombieHealthBar
            zombieHealthBar.DisableHealthBar();
            Debug.Log("Zombie died");
            ScoreManager.scoreCount += pointsGiven;
            // Disable the capsule collider
            Collider[] colliders = GetComponents<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        
                hasDied = true;

                StartCoroutine(DestroyAfterAnimation());
            }
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1); // Wait for the length of the "Die" animation

        Destroy(gameObject);
    }
}

