using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaFade : MonoBehaviour
{
    [SerializeField]
    private float maxAlpha;

    [SerializeField]
    private float minAlpha;

    [SerializeField]
    private float timePeriod;

    [SerializeField]
    private SpriteRenderer[] renderers;

    private float alphaStep;
    private float currentAlpha;
    private int currentAlphaDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAlphaDirection > 0 && currentAlpha >= maxAlpha || currentAlphaDirection < 0 && currentAlpha <= minAlpha)
        {
            currentAlphaDirection *= -1;
        }
        currentAlpha += alphaStep * currentAlphaDirection * Time.deltaTime;
        foreach(SpriteRenderer renderer in renderers)
        {
            Color c = renderer.color;
            c.a = currentAlpha;
            renderer.color = c;
        }
    }

    public void StartFading()
    {
        currentAlpha = maxAlpha;
        currentAlphaDirection = -1;
        alphaStep = (maxAlpha - minAlpha) / timePeriod;
    }
    
    public void StopFading()
    {
        foreach(SpriteRenderer renderer in renderers)
        {
            Color c = renderer.color;
            c.a = 1;
            renderer.color = c;
        }
    }
}
