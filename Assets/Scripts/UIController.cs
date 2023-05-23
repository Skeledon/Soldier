using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;

    [SerializeField]
    private Image armorBar;

    [SerializeField]
    private TextMeshProUGUI ammoAmount;

    [SerializeField]
    private Image reloadIndicator;

    [SerializeField]
    private UIWeaponSlot[] weaponsSlots;

    [SerializeField]
    private Color standardColor;
    [SerializeField]
    private Color outOfAmmoColor;


    [SerializeField]
    private SoldierController soldierController;

    [SerializeField]
    private Health soldierHealth;

    [SerializeField]
    private WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ReadAmmoAmount();
        ShowReload();
    }

    private void SetAmmoAmount(int magazine, int total)
    {
        ammoAmount.text = magazine + "/" + total;
        if (magazine + total == 0)
            ammoAmount.color = outOfAmmoColor;
        else
            ammoAmount.color = standardColor;
    }

    private void ReadAmmoAmount()
    {
        int magazine = weaponManager.CurrentWeapon().CurrentBulletsInMagazine;
        int total = weaponManager.CurrentWeapon().CurrentBulletsTotal;
        SetAmmoAmount(magazine, total);
    }

    private void ShowReload()
    {
        float currentTime = weaponManager.CurrentReloadTime;
        float maxTime = weaponManager.CurrentWeapon().reloadTime;
        float coeff = currentTime / maxTime;
        reloadIndicator.fillAmount = coeff;
    }
}
