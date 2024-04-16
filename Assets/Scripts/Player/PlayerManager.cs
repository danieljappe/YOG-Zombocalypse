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
        // Check if the loot already exists in the inventory
        InventoryItem foundItem = inventory.Find(item => item.item.name == loot.name);

        if (foundItem != null)
        {
            // If found, increase the quantity
            foundItem.quantity++;
        }
        else
        {
            // If not found, create a new inventory item
            InventoryItem newItem = new InventoryItem();
            newItem.item = loot;
            newItem.quantity = 1;
            inventory.Add(newItem);
        }

        // Remove the loot from its parent and deactivate it
        loot.transform.parent = null;
        loot.SetActive(false);
        Debug.Log("Picked up loot: " + loot.name);

        // Update the inventory UI
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
{
    // Reset the counts to zero before counting the items in the inventory
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

public int GetCoinCount()
    {
        int coinCount = 0;
        foreach (InventoryItem item in inventory)
        {
            if (item.item.CompareTag("Loot"))
            {
                coinCount += item.quantity;
            }
        }
        return coinCount;
    }

    public int GetHealthPackCount()
    {
        int healthPackCount = 0;
        foreach (InventoryItem item in inventory)
        {
            if (item.item.CompareTag("HealthPack"))
            {
                healthPackCount += item.quantity;
            }
        }
        return healthPackCount;
    }

    public void DeductCoins(int amount)
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (inventory[i].item.CompareTag("Loot"))
            {
                if (inventory[i].quantity >= amount)
                {
                    inventory[i].quantity -= amount;
                    break;
                }
                else
                {
                    amount -= inventory[i].quantity;
                    inventory.RemoveAt(i);
                }
            }
        }
    }

    public void DeductHealthPacks(int amount)
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (inventory[i].item.CompareTag("HealthPack"))
            {
                if (inventory[i].quantity >= amount)
                {
                    inventory[i].quantity -= amount;
                    break;
                }
                else
                {
                    amount -= inventory[i].quantity;
                    inventory.RemoveAt(i);
                }
            }
        }
    }
}

