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
    private Gradient healthBarColor;

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

    private bool uiInitialized = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (uiInitialized)
        {

            ReadCurrentWeaponAmmoAmount();
            ReadTotalAmmo();
            ShowReload();
            ShowHealth();
        }
    }

    public void InitializeUILinks(GameObject playerGo)
    {
        soldierController = playerGo.GetComponent<SoldierController>();
        soldierHealth = playerGo.GetComponent<Health>();
        weaponManager = playerGo.GetComponent<WeaponManager>();

        weaponManager.WeaponChanged += WeaponChanged;
        weaponManager.WeaponCollected += WeaponCollected;
        weaponManager.WeaponResetted += ResetWeponsSlots;

        uiInitialized = true;
    }

    private void SetCurrentWeaponAmmoAmount(int magazine, int total, bool infiniteMagazine, bool infiniteStock)
    {
        string magazineAmount;
        string totalAmount;
        if(infiniteMagazine)
        {
            magazineAmount = "∞";
        }
        else
        {
            magazineAmount = magazine.ToString();
        }

        if (infiniteStock)
        {
            totalAmount = "∞";
        }
        else
        {
            totalAmount = total.ToString();
        }

        ammoAmount.text = magazineAmount + "/" + totalAmount;
        if (magazine + total == 0)
            ammoAmount.color = outOfAmmoColor;
        else
            ammoAmount.color = standardColor;
    }

    private void ReadCurrentWeaponAmmoAmount()
    {
        int magazine = weaponManager.CurrentWeapon.CurrentBulletsInMagazine;
        int total = weaponManager.CurrentWeapon.CurrentBulletsInStock;
        bool infiniteMagazine = weaponManager.CurrentWeapon.HasInfiniteMagazineBullets;
        bool infiniteStock = weaponManager.CurrentWeapon.HasInfiniteMaxBullets;
        SetCurrentWeaponAmmoAmount(magazine, total, infiniteMagazine, infiniteStock);
    }

    private void ReadTotalAmmo()
    {
        foreach(Weapon w in weaponManager.AllWeaponsHeld)
        {
            if(w == null)
                continue;
            int ammo = w.CurrentBulletsTotal;
            weaponsSlots[w.weaponSlot].SetAmmo(ammo);
        }
    }

    private void ShowReload()
    {
        float currentTime = weaponManager.CurrentReloadTime;
        float maxTime = weaponManager.CurrentWeapon.reloadTime;
        float coeff = currentTime / maxTime;
        reloadIndicator.fillAmount = coeff;
    }

    private void ShowHealth()
    {
        float hpBarCoeff = (float)soldierHealth.CurrentHealth / soldierHealth.GetMaxHealth();
        hpBar.fillAmount = hpBarCoeff;
        hpBar.color = healthBarColor.Evaluate(hpBarCoeff);
        armorBar.fillAmount = (float)soldierHealth.CurrentArmor / soldierHealth.GetMaxArmor();
    }

    private void ResetWeponsSlots()
    {
        foreach(UIWeaponSlot slot in weaponsSlots)
        {
            slot.Show(false);
        }
    }

    private void WeaponChanged(int index)
    {
        foreach (UIWeaponSlot slot in weaponsSlots)
        {
            slot.Highlight(false);
        }
        weaponsSlots[index].Highlight(true);
    }

    private void WeaponCollected(Weapon w)
    {
        weaponsSlots[w.weaponSlot].SetInfiniteAmmo(w.HasInfiniteMaxBullets);
        weaponsSlots[w.weaponSlot].SetImage(w.Sprite);
        weaponsSlots[w.weaponSlot].Show(true);


    }

}
