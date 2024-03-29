using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBar healthBar;

    public GameObject gameOverCanvas; 

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //gameOverCanvas.SetActive(false);
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Player took " + damageAmount + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle player death
        Debug.Log("Player died");
        
        // Pause the game by setting the time scale to 0
        Time.timeScale = 0f;
        
        // Display game over screen
        gameOverCanvas.SetActive(true);
    }


    void OnTriggerEnter(Collider other)
    {
        ZombieController zombie = other.GetComponentInParent<ZombieController>();
        if (zombie != null && other.CompareTag("ZombieAttack"))
        {
            if (zombie.IsAttacking())
            {
                Debug.Log("Player hit by zombie's attack collider while zombie is attacking.");
                // Access the attack damage from the zombie
                TakeDamage(zombie.AttackDamage); // Call the TakeDamage method to apply damage to the player
            }
        }
    }
}