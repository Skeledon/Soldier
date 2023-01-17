using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float LifeTime;

    private ShotsPool myPool;
    private Transform t;
    private float currentTime;
    private GameObject owner;

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
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
