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


    private Transform t;



    #region Unity Methods
    private void Awake()
    {
        t = transform;
        legsAnimator = legs.GetComponent<Animator>();
        Spawn();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region public methods
    public void Move(Vector2 dir)
    {
        if (dir != Vector2.zero)
        {
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
    #endregion

    #region private methods

    private void RotateLegs(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        legs.rotation = Quaternion.AngleAxis(angle, t.forward);
    }
        
    #endregion

}
