using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PawnController))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int maxArmor;

    [SerializeField]
    private ParticleSystem bloodParticle;

    [SerializeField]
    private int numberOfBloodParticles = 30;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip damageSound;

    [SerializeField]
    private AudioClip deathSound;

    [SerializeField]
    private GameObject hpBarPrefab;

    [SerializeField]
    private Vector3 hpBarOffset;

    private HpBar myHpBar;

    public int CurrentHealth { get; private set; }
    public int CurrentArmor { get; private set; }

    private PawnController myController;
    private void Awake()
    {
        myController = GetComponent<PawnController>();
        Init();
    }

    public void Init()
    {
        CurrentHealth = maxHealth;
        CurrentArmor = maxArmor;
        myHpBar = GameObject.Instantiate(hpBarPrefab, transform.position + hpBarOffset, Quaternion.identity, transform).GetComponent<HpBar>();
        myHpBar.SetHPCoeff((float)CurrentHealth / maxHealth);
    }
    public void ApplyDamage(int dmg)
    {
        if (myController.CanTakeDamage())
        {
            float splitDmg = 0;
            int healthDmg;
            if (CurrentArmor == 0)
            {
                healthDmg = dmg; // if there is no armor all dmg goes directly to the health
            }
            else
            {
                float coeff = (float)CurrentArmor / maxArmor;
                splitDmg = (Mathf.Atan(6 * (coeff - .5f)) / Mathf.PI) * .3f + .62f; //calculated parametrically
                int armorDmg = Mathf.FloorToInt(dmg * splitDmg);
                int armorPassThroughDmg = Mathf.Clamp(armorDmg - CurrentArmor, 0, dmg); // damage that can't be absorbed by armor passes through to health
                healthDmg = Mathf.FloorToInt(dmg * (1 - splitDmg)) + armorPassThroughDmg;
                CurrentArmor = Mathf.Max(CurrentArmor - armorDmg, 0);
            }
            CurrentHealth = Mathf.Max(CurrentHealth - healthDmg, 0);
            bloodParticle.Emit(numberOfBloodParticles);
            audioSource.PlayOneShot(damageSound);
            myHpBar.SetHPCoeff((float)CurrentHealth / maxHealth);
            CheckDead();
            Debug.Log("health: " + CurrentHealth + " armor: " + CurrentArmor + " split: " + splitDmg);
        }
    }

    public void Heal(int heal)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + heal, maxHealth);
        myHpBar.SetHPCoeff((float)CurrentHealth / maxHealth);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetMaxArmor()
    {
        return maxArmor;
    }

    private void CheckDead()
    {
        if(CurrentHealth <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            myController.Die();
        }
    }
}
