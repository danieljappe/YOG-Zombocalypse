using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to add this line if not already added

public class PlayerManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public HealthBar healthBar;

    public GameObject gameOverCanvas; 

    public List<InventoryItem> inventory;

    public Text coinText;
    public Text healthPackText;

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

        if(other.CompareTag("Loot") || other.CompareTag("HealthPack")){
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

    void PickUpLoot(GameObject loot)
    {
        InventoryItem foundItem = inventory.Find(item => item.item == loot);
        if (foundItem != null)
        {
            foundItem.quantity++;
        }
        else
        {
            InventoryItem newItem = new InventoryItem();
            newItem.item = loot;
            newItem.quantity = 1;
            inventory.Add(newItem);
        }

        loot.transform.parent = null;
        loot.SetActive(false);
        Debug.Log("Picked up loot: " + loot.name);

        // Update the inventory UI
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
{
    // Initialize counters for coins and health packs
    int coinCount = 0;
    int healthPackCount = 0;

    // Count the number of coins and health packs in the inventory
    foreach (InventoryItem item in inventory)
    {
        if (item.item.CompareTag("Loot"))
        {
            coinCount += item.quantity;
        }
        else if (item.item.CompareTag("HealthPack"))
        {
            healthPackCount += item.quantity;
        }
    }

    // Update the coin UI text with the coin count
    coinText.text = "x " + coinCount;

    // Update the health pack UI text with the health pack count
    healthPackText.text = "x " + healthPackCount;
}

}
