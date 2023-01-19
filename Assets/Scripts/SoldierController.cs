using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform legs;

    [SerializeField]
    private WeaponManager weaponManager;


    private Animator legsAnimator;
    private Animator headAnimator;


    private Transform t;



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
    void Update()
    {
        
    }

    #endregion

    #region public methods
    public void Move(Vector2 dir, bool relativeMovement)
    {
        if (dir != Vector2.zero)
        {
            if (relativeMovement)
            {
                dir = head.transform.rotation * dir;
            }
            if (dir.sqrMagnitude > 1)
                dir.Normalize();
            t.Translate(dir * speed * Time.deltaTime);
            RotateLegs(dir);
            legsAnimator.SetBool("isWalking", true);
        }
        else
        {
            legsAnimator.SetBool("isWalking", false);
        }
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
    }

    public void Die()
    {
        headAnimator.SetTrigger("Death");
    }

    public void ChangeWeapon(int index)
    {
        weaponManager.ChangeWeapon(index);
    }
    #endregion

    #region private methods

    private void RotateLegs(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        legs.rotation = Quaternion.AngleAxis(angle, t.forward);
    }
        
    #endregion

}
