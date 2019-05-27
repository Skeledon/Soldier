using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairMousePositionLink : MonoBehaviour
{
    public Camera MainCam;

    private Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        t.position = MainCam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }
}
