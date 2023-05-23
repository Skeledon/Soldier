using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float respawnInvulnerabilityTime;

    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform legs;

    [SerializeField]
    private WeaponManager weaponManager;

    [SerializeField]
    private AlphaFade fade;

    private enum SoldierState { ALIVE, DEAD, INVULNERABLE }
    private SoldierState state;

    private Animator legsAnimator;
    private Animator headAnimator;


    private Transform t;

    private Vector2 direction;



    #region Unity Methods
    private void Awake()
    {
        t = transform;
        legsAnimator = legs.GetComponent<Animator>();
        headAnimator = head.GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ExecuteMovement();
    }

    #endregion

    #region public methods
    public void Move(Vector2 dir, bool relativeMovement)
    {
        if (relativeMovement)
        {
            dir = head.transform.rotation * dir;
        }
        direction = dir;
    }

    public void RotateAimTowards(Vector2 target)
    {
        Vector2 vectorToTarget = target - (Vector2)t.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        head.rotation = Quaternion.AngleAxis(angle, t.forward);
    }

    public void Shoot()
    {
        weaponManager.Shoot(head.position, head.rotation, gameObject);
    }

    public void Spawn()
    {
        weaponManager.WeaponCollected(0);
        state = SoldierState.INVULNERABLE;
        headAnimator.SetTrigger("Respawn");
        StartCoroutine(WaitForInvulnerabilityTime());
    }

    public void Die()
    {
        headAnimator.SetTrigger("Death");
    }

    public void ChangeWeapon(int index)
    {
        weaponManager.ChangeWeapon(index);
    }

    public bool CanTakeDamage()
    {
        if (state == SoldierState.ALIVE)
            return true;
        return false;
    }

    #endregion

    #region private methods

    private void RotateLegs(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        legs.rotation = Quaternion.AngleAxis(angle, t.forward);
    }

    private void ExecuteMovement()
    {
        if (direction != Vector2.zero)
        {

            if (direction.sqrMagnitude > 1)
                direction.Normalize();
            t.Translate(direction * speed * Time.deltaTime);
            RotateLegs(direction);
            legsAnimator.SetBool("isWalking", true);
        }
        else
        {
            legsAnimator.SetBool("isWalking", false);
        }
    }

    private IEnumerator WaitForInvulnerabilityTime()
    {
        fade.enabled = true;
        fade.StartFading();
        yield return new WaitForSeconds(respawnInvulnerabilityTime);
        state = SoldierState.ALIVE;
        fade.StopFading();
        fade.enabled = false;
    }
        
    #endregion

}
