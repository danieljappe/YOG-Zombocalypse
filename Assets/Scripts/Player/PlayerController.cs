using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private WeaponManager weaponManager; // Reference to the WeaponManager script
    private GunController currentGun; // Reference to the current weapon's GunController
    private bool isFiring; // Flag to track if the player is currently firing
    private float fireTimer; // Timer to control the firing rate
    private bool canFire = true; // Flag to control if the player can fire

    void Start()
    {
        // Get a reference to the WeaponManager script
        weaponManager = GetComponent<WeaponManager>();
        
        // Get the GunController of the initial weapon
        if (weaponManager != null && weaponManager.weapons.Count > 0)
        {
            currentGun = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<GunController>();
            Debug.Log("Current Gun: " + currentGun.name); // Add this line to check the currentGun
        }
        else
        {
            Debug.LogWarning("WeaponManager or weapons list is not set up correctly!");
        }
    }
    
    void Update()
    {
        // Check if the player can fire
        if (!canFire)
            return;

        Vector3 mousePos = Input.mousePosition;
        currentGun = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<GunController>();
        // Check if there is a current gun
        if (currentGun != null)
        {
            // Check if the fire button is being held down
            if (Input.GetButton("Fire1"))
            {
                // Start firing
                isFiring = true;
            }
            else
            {
                // Stop firing
                isFiring = false;
            }

            // If the player is firing and the fire timer has elapsed, fire the gun
            if (isFiring && fireTimer <= 0)
            {
                currentGun.isFiring = true;
                fireTimer = currentGun.timeBetweenShots; // Reset the fire timer
            }
            else
            {
                currentGun.isFiring = false;
            }

            // Update the fire timer
            if (fireTimer > 0)
            {
                fireTimer -= Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("currentGun is null"); // Add this line to check if currentGun is null
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0f; 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir), rotationSpeed * Time.deltaTime);
        }
    }

    // Method to enable or disable firing
    public void SetCanFire(bool value)
    {
        canFire = value;
    }
}
