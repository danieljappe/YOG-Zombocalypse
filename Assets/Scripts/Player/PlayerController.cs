using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private WeaponManager weaponManager; // Reference to the WeaponManager script
    private GunController currentGun; // Reference to the current weapon's GunController


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
        Vector3 mousePos = Input.mousePosition;
        currentGun = weaponManager.weapons[weaponManager.currentWeaponIndex].GetComponent<GunController>();
        // Check if there is a current gun and if it's firing
        if (currentGun != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                currentGun.isFiring = true;
                Debug.Log("Firing: " + currentGun.name); // Add this line to check firing
            }

            if (Input.GetButtonUp("Fire1"))
            {
                currentGun.isFiring = false;
                Debug.Log("Not Firing: " + currentGun.name); // Add this line to check not firing
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
}
