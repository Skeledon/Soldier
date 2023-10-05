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
    UnityEngine.UI.Image weaponImage;

    [SerializeField]
    Vector2 alphaValues;


    private int ammoAmount;
    private bool infiniteAmmo;

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

        if(infiniteAmmo)
        {
            ammoText.text = "∞";
        }
        else
        {
            ammoAmount = amount;
            ammoText.text = ammoAmount.ToString();
        }
    }

    public void SetInfiniteAmmo()
    {
        SetInfiniteAmmo(true);
    }

    public void SetInfiniteAmmo(bool b)
    {
        infiniteAmmo = b;
    }

    public void SetImage(Sprite s)
    {
        weaponImage.sprite = s;
    }
}
