using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData[] WeaponsData;

    public float CurrentReloadTime { get; private set; }

    private GameObject ShotsFather;
    
    private Weapon[] weaponsHeld = new Weapon[10];
    private int currentWeapon = 0;
    private bool canFire = true;
    private Coroutine currentWaitCoroutine;

    private void Awake()
    {
        ShotsFather = GameObject.FindGameObjectWithTag("ShotPool");
        ResetWeapons();
    }
    public bool WeaponCollected(int slot)
    {
        if (weaponsHeld[slot] != null)
            return false;
        weaponsHeld[slot] = new Weapon(WeaponsData[slot], ShotsFather);
        return true;
    }

    public void Shoot(Vector3 position, Quaternion rotation, GameObject owner)
    {
        if (canFire)
        {
            int currentAmmo = weaponsHeld[currentWeapon].Shoot(position, rotation, owner);
            if (currentAmmo > 0)
            {
                currentWaitCoroutine = StartCoroutine(WaitForNextShot(weaponsHeld[currentWeapon].timeBetweenShots));
            }
            else
            {
                if (weaponsHeld[currentWeapon].CurrentBulletsTotal != 0)
                {
                    currentWaitCoroutine = StartCoroutine(WaitForReload(weaponsHeld[currentWeapon].reloadTime));
                }
                else
                {
                    canFire = false;
                }
            }
        }
    }

    public void ChangeWeapon(int slot)
    {
        if (weaponsHeld[slot] == null)
            return;
        currentWeapon = slot;
        StopCoroutine(currentWaitCoroutine);
        CurrentReloadTime = 0f;
        canFire = true;
        if (weaponsHeld[slot].CurrentBulletsInMagazine == 0 && weaponsHeld[slot].CurrentBulletsTotal != 0)
        {
            currentWaitCoroutine = StartCoroutine(WaitForReload(weaponsHeld[currentWeapon].reloadTime));
        }
    }

    public void ResetWeapons()
    {
        for(int i = 0; i < weaponsHeld.Length; i++)
        {
            weaponsHeld[i] = null;
        }
    }

    IEnumerator WaitForNextShot(float time)
    {
        canFire = false;
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    IEnumerator WaitForReload(float reloadTime)
    {
        canFire = false;
        CurrentReloadTime = 0f;
        while (CurrentReloadTime < reloadTime)
        {
            CurrentReloadTime += Time.deltaTime;
            yield return null;
        }
        canFire = true;
        CurrentReloadTime = 0f;
        weaponsHeld[currentWeapon].Reload();
    }

    public Weapon CurrentWeapon()
    {
        return weaponsHeld[currentWeapon];
    }
}
