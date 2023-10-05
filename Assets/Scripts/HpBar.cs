using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Gradient colors;

    [SerializeField]
    private SpriteRenderer myRenderer;

    private float hpCoeff;


    public void SetHPCoeff(float hp)
    {
        hpCoeff = hp;
        transform.localScale = new Vector3 (hpCoeff, transform.localScale.y, transform.localScale.z);
        myRenderer.color = colors.Evaluate(hp);
    }
}
