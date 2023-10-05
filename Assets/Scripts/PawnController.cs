using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    [SerializeField]
    protected float speed;

    [SerializeField]
    protected float respawnInvulnerabilityTime;

    [SerializeField]
    protected WeaponManager weaponManager;

    protected enum CharacterState { ALIVE, DEAD, INVULNERABLE }
    protected CharacterState state;

    protected Transform t;

    protected Vector2 direction;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Shoot()
    {

    }

    public virtual void Spawn()
    {

    }

    public virtual void Die()
    {

    }

    public void ChangeWeapon(int index)
    {
        weaponManager.ChangeWeapon(index);
    }

    public void ReloadWeapon()
    {
        weaponManager.ReloadWeapon();
    }

    public bool CanTakeDamage()
    {
        if (state == CharacterState.ALIVE)
            return true;
        return false;
    }
}
