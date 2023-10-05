using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public int MagazineSize { get; private set; }
    public int CurrentBulletsInMagazine { get; private set; }
    public int MaxBullets { get; private set; }
    public int CurrentBulletsInStock { get; private set; }
    public AudioClip ShotSound { get; private set; }

    public Sprite Sprite { get; private set; }

    public int CurrentBulletsTotal { get { return CurrentBulletsInStock + CurrentBulletsInMagazine; } }

    private ShotsPool pool;
    private string weaponName;
    private float fireRate;
    private float deviation;
    private GameObject shotPrefab;
    private GameObject shotsFather;

    public int weaponSlot { get; }
    public float reloadTime { get; }

    public float timeBetweenShots { get { return 1 / fireRate; } }

    private bool canFire;

    public bool HasInfiniteMaxBullets { get { return MaxBullets < 0; } }
    public bool HasInfiniteMagazineBullets { get { return MagazineSize < 0; } }

    public Weapon(WeaponData data, GameObject shotsFather)
    {
        weaponName = data.Name;
        MagazineSize = data.MagazineSize;
        CurrentBulletsInMagazine = MagazineSize;
        MaxBullets = data.MaxBullets;
        CurrentBulletsInStock = data.StartingBullets;
        reloadTime = data.ReloadTime;
        fireRate = data.FireRate;
        shotPrefab = data.ShotPrefab;
        weaponSlot = data.WeaponSlot;
        deviation = data.Deviation;
        ShotSound = data.ShotClip;
        Sprite = data.WeaponSprite;
        this.shotsFather = shotsFather;

        pool = new ShotsPool();
        pool.Init(MaxBullets, shotPrefab, shotsFather);
        canFire = true;
    }

    public int Shoot(Vector3 position, Quaternion rotation, GameObject owner)
    {
        if(CurrentBulletsInMagazine == 0)
            return 0;
        float rnd = Random.Range(-deviation, deviation);
        Vector3 eulerRotation = rotation.eulerAngles;
        eulerRotation += new Vector3(0, 0, rnd);
        rotation = Quaternion.Euler(eulerRotation);
        pool.ShotFired(position, rotation, owner);
        UpdateAmmo();
        return CurrentBulletsInMagazine;
    }

    private void UpdateAmmo()
    {
        CurrentBulletsInMagazine--;
        if (CurrentBulletsInMagazine == 0)
        {
            canFire = false;
        }
    }
    public void Reload()
    {
        int ammoToPutInMagazine;
        if (!HasInfiniteMaxBullets)
        {
            ammoToPutInMagazine = Mathf.Min(MagazineSize - CurrentBulletsInMagazine, CurrentBulletsInStock);
            CurrentBulletsInStock -= ammoToPutInMagazine;
        }
        else
        {
            ammoToPutInMagazine = MagazineSize;
        }
        CurrentBulletsInMagazine += ammoToPutInMagazine;
    }
}
