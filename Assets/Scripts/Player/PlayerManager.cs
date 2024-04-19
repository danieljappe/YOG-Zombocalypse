using System.Collections;
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
    public int healAmount;

    public Text popUpTextPrefab; // Assign your PopUpText prefab here in the Inspector
    public Transform popUpTextParent; // Assign the parent object for pop-up texts in the Inspector

    private bool isPopUpTextVisible = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        //gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        // Check if the player presses the heal key (e.g., 'H')
        if (Input.GetKeyDown(KeyCode.H))
        {
            // Check if a pop-up text is already visible
            if (!isPopUpTextVisible)
            {
                // Set the flag to indicate that a pop-up text is being displayed
                isPopUpTextVisible = true;

                // Check if the player has health packs available
                if (GetHealthPackCount() > 0)
                {
                    // Check if the player's health is less than maximum
                    if (currentHealth < maxHealth)
                    {
                        // If all conditions are met, heal the player, deduct a health pack, and update UI
                        Heal(healAmount);
                        DeductHealthPacks(1);
                        UpdateInventoryUI();
                    }
                    else
                    {
                        // Provide feedback to the player that they are already at full health
                        TriggerPopUpText("You are already at full health", 3f);
                    }
                }
                else
                {
                    // Provide feedback to the player that they have no health packs left
                    TriggerPopUpText("You have no health packs", 3f);
                }

                // Start a coroutine to reset the isPopUpTextVisible flag after the duration
                StartCoroutine(ResetPopUpTextFlag(3f));
            }
        }
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

    void Heal(int healAmount)
    {
        currentHealth += healAmount;
        healthBar.SetHealth(currentHealth);
        
        if (!isPopUpTextVisible)
        {
            isPopUpTextVisible = true;
            
            TriggerPopUpText("You healed for " + healAmount + " HP", 3);

            StartCoroutine(ResetPopUpTextFlag(3f));
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        
        Time.timeScale = 0f;

        gameOverCanvas.SetActive(true);
    }

    private bool firstHealthPack = false;
    void PickUpLoot(GameObject loot)
    {
        // Check if the loot already exists in the inventory
        InventoryItem foundItem = inventory.Find(item => item.item.name == loot.name);

        if(!firstHealthPack && !isPopUpTextVisible && loot.CompareTag("HealthPack")){
            TriggerPopUpText("You have picked up a Health Pack, Press H to use",3);
            firstHealthPack=true;
            isPopUpTextVisible=true;
            StartCoroutine(ResetPopUpTextFlag(3f));
        }

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

    IEnumerator ShowPopUpText(string message, float duration)
    {
        // Instantiate the pop-up text prefab
        Text popUpTextInstance = Instantiate(popUpTextPrefab, popUpTextParent);

        // Set the pop-up text message
        popUpTextInstance.text = message;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Destroy the pop-up text object after the duration
        Destroy(popUpTextInstance.gameObject);
    }

    // Method to trigger the pop-up text
    public void TriggerPopUpText(string message, float duration)
    {
        StartCoroutine(ShowPopUpText(message, duration));
    }

    IEnumerator ResetPopUpTextFlag(float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Reset the flag to indicate that the pop-up text is no longer visible
        isPopUpTextVisible = false;
    }
}

