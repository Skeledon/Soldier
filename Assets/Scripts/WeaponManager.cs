using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData[] WeaponsData;

    private GameObject ShotsFather;
    
    private Weapon[] weaponsHeld = new Weapon[10];
    private int currentWeapon = 0;
    private bool canFire = true;

    private void Awake()
    {
        ShotsFather = GameObject.FindGameObjectWithTag("ShotPool");
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
        if(canFire)
        {
            weaponsHeld[currentWeapon].Shoot(position, rotation, owner);
            canFire = false;
            StartCoroutine(WaitForNextShot(weaponsHeld[currentWeapon].timeBetweenShots));
        }
    }

    public void ChangeWeapon(int slot)
    {
        if (weaponsHeld[slot] == null)
            return;
        currentWeapon = slot;
    }

    IEnumerator WaitForNextShot(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}
