using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float rotationSpeed = 5f;

    public GunController theGun;

    void Update()
    {
        
        Vector3 mousePos = Input.mousePosition;

        if(Input.GetButtonDown("Fire1")){
            theGun.isFiring = true;
        }

        if(Input.GetButtonUp("Fire1")){
            theGun.isFiring = false;
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
