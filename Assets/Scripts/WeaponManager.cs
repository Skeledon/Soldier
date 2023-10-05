using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData[] WeaponsData;

    public float CurrentReloadTime { get; private set; }

    [SerializeField]
    private bool isPlayerWeaponManager;

    [SerializeField]
    private AudioSource audioSource;

    private GameObject ShotsFather;

    private Weapon[] weaponsHeld = new Weapon[4];
    private int currentWeapon = 0;
    private bool canFire = true;
    private Coroutine currentWaitCoroutine;

    public Weapon CurrentWeapon
    {
        get
        {
            return weaponsHeld[currentWeapon];
        }
    }

    public Weapon[] AllWeaponsHeld
    {
        get
        {
            Weapon[] w = (Weapon[])weaponsHeld.Clone();
            return w;
        }
    }

    //events for playerui
    public delegate void WeaponChange(int index);
    public event WeaponChange WeaponChanged;

    public delegate void WeaponReset();
    public event WeaponReset WeaponResetted;

    public delegate void WeaponCollect(Weapon w);
    public event WeaponCollect WeaponCollected;

    private void Awake()
    {
        ShotsFather = GameObject.FindGameObjectWithTag("ShotPool");
    }
    public bool CollectWeapon(int slot)
    {
        if (weaponsHeld[slot] != null)
            return false;
        weaponsHeld[slot] = new Weapon(WeaponsData[slot], ShotsFather);
        if (isPlayerWeaponManager)
            WeaponCollected.Invoke(weaponsHeld[slot]);
        return true;
    }

    public void Shoot(Vector3 position, Quaternion rotation, GameObject owner)
    {
        if (canFire)
        {
            int currentAmmo = weaponsHeld[currentWeapon].Shoot(position, rotation, owner);
            audioSource.PlayOneShot(CurrentWeapon.ShotSound);
            if (currentAmmo > 0 || weaponsHeld[currentWeapon].HasInfiniteMagazineBullets)
            {
                currentWaitCoroutine = StartCoroutine(WaitForNextShot(weaponsHeld[currentWeapon].timeBetweenShots));
            }
            else
            {
                if (weaponsHeld[currentWeapon].CurrentBulletsInStock != 0)
                {
                    ReloadWeapon();
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
        if (isPlayerWeaponManager)
            WeaponChanged.Invoke(slot);
        currentWeapon = slot;
        if(currentWaitCoroutine != null)
            StopCoroutine(currentWaitCoroutine);
        CurrentReloadTime = 0f;
        canFire = true;
        if (weaponsHeld[slot].CurrentBulletsInMagazine == 0)
        {
            if(weaponsHeld[slot].CurrentBulletsInStock != 0)
                currentWaitCoroutine = StartCoroutine(WaitForReload(weaponsHeld[currentWeapon].reloadTime));
            else
                canFire = false;
        }

    }

    public void ResetWeapons()
    {
        for(int i = 0; i < weaponsHeld.Length; i++)
        {
            weaponsHeld[i] = null;
        }
        if (isPlayerWeaponManager)
            WeaponResetted.Invoke();
    }

    public void ReloadWeapon()
    {
        if (weaponsHeld[currentWeapon].CurrentBulletsInMagazine == weaponsHeld[currentWeapon].MagazineSize || weaponsHeld[currentWeapon].CurrentBulletsInStock == 0)
            return;
        if (CurrentReloadTime == 0f)
        {
            currentWaitCoroutine = StartCoroutine(WaitForReload(weaponsHeld[currentWeapon].reloadTime));
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


}
