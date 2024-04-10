using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons/Weapon")]
public class WeaponData : ScriptableObject
{
    public BulletController bulletPrefab;
    public float bulletSpeed;
    public float timeBetweenShots;
    public float maxBulletLifetime;
}
