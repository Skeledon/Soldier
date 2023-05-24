using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponSlot : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    GameObject mainVisualObject;

    [SerializeField]
    TMPro.TextMeshProUGUI ammoText;

    [SerializeField]
    Vector2 alphaValues;


    private int ammoAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Highlight(bool state)
    {
        if (state)
        {
            canvasGroup.alpha = alphaValues.y;
        }
        else
        {
            canvasGroup.alpha = alphaValues.x;
        }
    }

    public void Show(bool state)
    {
        mainVisualObject.SetActive(state);
    }

    public void SetAmmo(int amount)
    {
        ammoAmount = amount;
        ammoText.text = ammoAmount.ToString();
    }
}
