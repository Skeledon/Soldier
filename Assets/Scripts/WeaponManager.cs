using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData[] WeaponsData;
    
    private Weapon[] weaponsHeld = new Weapon[10];
    private int currentWeapon = 0;
    private float currentFireTimer = 0f;
    private bool canFire = true;

    public void WeaponCollected(int slot)
    {
        if (weaponsHeld[slot] != null)
            return;
        weaponsHeld[slot] = new Weapon(WeaponsData[slot]);
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

    IEnumerator WaitForNextShot(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}
