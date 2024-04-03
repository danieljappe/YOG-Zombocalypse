using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBar healthBar;

    public GameObject gameOverCanvas; 

    public List<GameObject> inventory;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //gameOverCanvas.SetActive(false);
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

        if(other.CompareTag("Loot")){
            PickUpLoot(other.gameObject);
        }
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

    void PickUpLoot(GameObject loot){
        inventory.Add(loot); // Add the loot GameObject to the player's inventory
        loot.transform.parent = null; // Detach the loot from its parent
        loot.SetActive(false); // Deactivate the loot GameObject
        Debug.Log("Picked up loot: " + loot.name);
    }


    
}