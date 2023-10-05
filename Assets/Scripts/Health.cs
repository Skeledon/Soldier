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
    private AnimationCurve armorBehaviour;

    [SerializeField]
    private ParticleSystem bloodParticle;

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
            float coeff = CurrentArmor / maxArmor;
            float splitDmg = armorBehaviour.Evaluate(coeff);
            int armorDmg = Mathf.FloorToInt(dmg * splitDmg);
            int healthDmg = Mathf.FloorToInt(dmg * (1 - splitDmg));
            CurrentHealth = Mathf.Max(CurrentHealth - healthDmg, 0);
            CurrentArmor = Mathf.Max(CurrentArmor - armorDmg, 0);
            bloodParticle.Emit(30);
            audioSource.PlayOneShot(damageSound);
            myHpBar.SetHPCoeff((float)CurrentHealth / maxHealth);
            CheckDead();
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
