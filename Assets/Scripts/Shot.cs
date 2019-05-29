using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float LifeTime;

    private GameObject myPool;
    private Transform t;
    private float currentTime;

    private void Awake()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        t.Translate(t.up * Speed * Time.deltaTime);
        currentTime += Time.deltaTime;
        if (currentTime >= LifeTime)
            DestroySelf();
    }

    public void Init()
    {
        
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
