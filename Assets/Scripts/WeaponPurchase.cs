using TMPro;
using UnityEngine;

public class WeaponPurchase : MonoBehaviour
{
    public int weaponPrice;
    public TextMeshPro text;

    public TextMeshPro purchaseText;
    public GameObject weaponToPurchase;
    public WeaponManager weaponManager;

    public PlayerManager playerManager;

    private bool playerInsideCollider = false; //Track if the player is inside the collider for this specific weapon

    private bool weaponHasBeenBought = false;

    void Start()
    {
        // Set the initial text to display the weapon price
        UpdateText();
    }

    void UpdateText()
    {
        // Update the text to display the weapon price
        text.text = "Price: " + weaponPrice.ToString();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered collider");
            playerInsideCollider = true; //Set to true when the player enters the collider for this specific weapon
            if(!weaponHasBeenBought){
                text.enabled = true;
                purchaseText.enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exited Collider");
            playerInsideCollider = false;
            text.enabled = false;
            purchaseText.enabled = false;
        }
    }

    void Update()
    {
        if (playerInsideCollider && Input.GetKeyDown(KeyCode.B))
        {
            if(playerManager.GetCoinCount() >= weaponPrice){
                PurchaseWeapon();
            } else {
                Debug.Log("Not enough coins");
            }
        }
    }

    private void PurchaseWeapon()
    {
        //Deduct coins
        playerManager.DeductCoins(weaponPrice);

        //Update UI
        playerManager.UpdateInventoryUI();

        Debug.Log("Purchased weapon: " + weaponToPurchase.name);
        
        //Add weapon to inventory
        weaponManager.AddWeaponToInventory(weaponToPurchase);

        text.enabled = false;
        purchaseText.enabled = false;
        weaponHasBeenBought = true;
    }
}
