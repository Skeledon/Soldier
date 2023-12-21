using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : PawnController
{
    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform legs;

    [SerializeField]
    private AlphaFade fade;



    private Animator legsAnimator;
    private Animator headAnimator;



    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
        legsAnimator = legs.GetComponent<Animator>();
        headAnimator = head.GetComponent<Animator>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //Spawn();
    }

    private void Update()
    {

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

    public override void Shoot()
    {
        weaponManager.Shoot(head.position, head.rotation, gameObject);
    }

    public override void Spawn()
    {
        weaponManager.ResetWeapons();
        weaponManager.CollectWeapon(0);
        weaponManager.ChangeWeapon(0);
        state = CharacterState.INVULNERABLE;
        headAnimator.SetTrigger("Respawn");
        StartCoroutine(WaitForInvulnerabilityTime());
    }

    public override void Die()
    {
        headAnimator.ResetTrigger("Respawn");
        headAnimator.SetTrigger("Death");
        state = CharacterState.DEAD;
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
            t.Translate(direction * speed * Time.fixedDeltaTime);
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
        state = CharacterState.ALIVE;
        fade.StopFading();
        fade.enabled = false;
    }
        
    #endregion

}
