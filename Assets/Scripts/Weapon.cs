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
    private float deviation;
    private GameObject shotPrefab;
    private GameObject shotsFather;

    public int weaponSlot { get; }
    public float reloadTime { get; }

    public float timeBetweenShots { get { return 1 / fireRate; } }

    public Weapon(WeaponData data, GameObject shotsFather)
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
        deviation = data.Deviation;
        this.shotsFather = shotsFather;

        pool = new ShotsPool();
        pool.Init(maxBullets, shotPrefab, shotsFather);
    }

    public void Shoot(Vector3 position, Quaternion rotation, GameObject owner)
    {
        float rnd = Random.Range(-deviation, deviation);
        Vector3 eulerRotation = rotation.eulerAngles;
        eulerRotation += new Vector3(0, 0, rnd);
        rotation = Quaternion.Euler(eulerRotation);
        pool.ShotFired(position, rotation, owner);
    }
}
