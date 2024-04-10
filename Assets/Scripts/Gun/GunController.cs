using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    public BulletController bullet;
    public float bulletSpeed;
    public float timeBetweenShots;
    public float maxBulletLifetime;
    public Transform firePoint;

    private float shotCounter;

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;

                // Check if the current gun is a shotgun
                if (IsShotgun())
                {
                    StartCoroutine(FireShotgun());
                }
                else
                {
                    StartCoroutine(FireBullet());
                }
            }
        }
        else
        {
            shotCounter = 0;
        }
    }

    IEnumerator FireBullet()
    {
        Debug.Log("Firing bullet: " + bullet.name); // Add this line to check firing bullet
        
        // Calculate the direction towards the cursor point
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        Vector3 direction = Vector3.zero;
        if (Physics.Raycast(ray, out hit))
        {
            direction = (hit.point - firePoint.position).normalized;
        }
        else
        {
            direction = (ray.GetPoint(1000f) - firePoint.position).normalized;
        }

        BulletController newBullet = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));
        newBullet.speed = bulletSpeed;

        yield return new WaitForSeconds(maxBulletLifetime);
        if (newBullet)
        {
            Destroy(newBullet.gameObject);
        }
    }

    IEnumerator FireShotgun()
    {
        Debug.Log("Firing shotgun: " + bullet.name); // Add this line to check firing shotgun

        for (int i = 0; i < 3; i++)
        {
            // Calculate direction with a small deviation for each bullet
            Vector3 deviation = Random.insideUnitSphere * 0.5f;
            Vector3 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            Vector3 direction = Vector3.zero;
            if (Physics.Raycast(ray, out hit))
            {
                direction = ((hit.point - firePoint.position) + deviation).normalized;
            }
            else
            {
                direction = ((ray.GetPoint(1000f) - firePoint.position) + deviation).normalized;
            }

            BulletController newBullet = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(direction));
            newBullet.speed = bulletSpeed;

            yield return new WaitForSeconds(0.01f); // Small delay between each shotgun bullet
        }
    }

    bool IsShotgun()
    {
         if (gameObject.CompareTag("Shotgun"))
    {
        return true; // Gun is a shotgun
    }
    else
    {
        return false; // Gun is not a shotgun
    }
    }
}
