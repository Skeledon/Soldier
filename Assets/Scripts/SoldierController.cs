using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Transform t;

    private void Awake()
    {
        t = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 dir)
    {
        t.Translate(dir * speed * Time.deltaTime);
    }
}
