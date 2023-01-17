using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private ShotsPool pool;
    private string weaponName;
    private int magazineSize;
    private int currentBulletsInMagazine;
    private int maxBullets;
    private int currentBulletsTotal;
    private float fireRate;
    private GameObject shotPrefab;

    public int weaponSlot { get; }
    public float reloadTime { get; }

    public float timeBetweenShots { get { return 1 / fireRate; } }

    public Weapon(WeaponData data)
    {
        weaponName = data.Name;
        magazineSize = data.MagazineSize;
        currentBulletsInMagazine = magazineSize;
        maxBullets = data.MaxBullets;
        currentBulletsTotal = data.StartingBullets;
        reloadTime = data.ReloadTime;
        fireRate = data.FireRate;
        shotPrefab = data.ShotPrefab;
        weaponSlot = data.WeaponSlot;

        pool = new ShotsPool();
        pool.Init(maxBullets, shotPrefab);
    }

    public void Shoot(Vector3 position, Quaternion rotation, GameObject owner)
    {
        pool.ShotFired(position, rotation, owner);
    }
}
