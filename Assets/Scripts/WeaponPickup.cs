using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField]
    private int WeaponSlot;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Soldier"))
        {
            if (collision.GetComponent<WeaponManager>().CollectWeapon(WeaponSlot))
                DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
