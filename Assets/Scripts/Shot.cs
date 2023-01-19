using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float LifeTime;
    [SerializeField]
    private int Damage;

    private ShotsPool myPool;
    private Transform t;
    private float currentTime;
    private GameObject owner;

    private bool alive;

    private void Awake()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        t.Translate(t.up * Speed * Time.deltaTime, Space.World);
        currentTime += Time.deltaTime;
        if (currentTime >= LifeTime)
            DestroySelf();
    }

    public void Init(GameObject own, ShotsPool myPool)
    {
        owner = own;
        this.myPool = myPool;
        currentTime = 0;
        alive = true;
    }

    private void DestroySelf()
    {
        alive = false;
        myPool.ShotDestroyed(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Soldier") && collision.gameObject != owner && alive)
        {
            collision.GetComponent<Health>().ApplyDamage(Damage);
            DestroySelf();
        }
        if(collision.CompareTag("Wall"))
        {
            DestroySelf();
        }
    }
}
