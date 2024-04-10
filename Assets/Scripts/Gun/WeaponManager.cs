using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons; // List of weapon GameObjects

    public int currentWeaponIndex = 0;
    
    public GunController theGun;

    void Start()
    {
        // Enable the first weapon and disable others
        SwitchWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Example: Switch weapon when pressing number keys (1, 2, 3, etc.)
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SwitchWeapon(i);
                break;
            }
        }
    }

    void SwitchWeapon(int index)
    {
        // Disable the previous weapon
        if (theGun != null)
            theGun.gameObject.SetActive(false);

        // Enable the chosen weapon
        weapons[index].SetActive(true);
        Debug.Log("Switched to weapon: " + weapons[index].name); // Add this line to check switching

        // Update currentWeaponIndex
        currentWeaponIndex = index;

        // Get the GunController component of the current weapon
        theGun = weapons[index].GetComponent<GunController>();

        // Ensure theGun is not null
        if (theGun == null)
        {
            Debug.LogError("The current weapon does not have a GunController component!");
            return;
        }
        else
        {
            Debug.Log("GunController found: " + theGun.name); // Add this line to check if GunController is found
        }

        // Ensure that theGun is firing if the player was firing the previous weapon
        if (theGun.isFiring)
            theGun.isFiring = false; // Stop firing the previous weapon

        // Assign theGun to the current weapon
        theGun.gameObject.SetActive(true);
    }
}
