using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isFiring;
    public BulletController bullet;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float timeBetweenShots;
    [SerializeField] private float shotCounter;
    [SerializeField] public float maxBulletLifetime; //Amount of time the bullet exists
    public Transform firePoint;

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                StartCoroutine(FireBullet());
            }
        }
        else
        {
            shotCounter = 0;
        }
    }

    IEnumerator FireBullet()
    {
        BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        newBullet.speed = bulletSpeed;
        yield return new WaitForSeconds(maxBulletLifetime);
        Destroy(newBullet.gameObject);
    }
}