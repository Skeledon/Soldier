using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoldierController))]
public class Health : MonoBehaviour
{
    [SerializeField]
    private int MaxHealth;

    [SerializeField]
    private ParticleSystem bloodParticle;

    public int CurrentHealth { get; private set; }

    private SoldierController myController;
    private void Awake()
    {
        myController = GetComponent<SoldierController>();
        Init();
    }

    public void Init()
    {
        CurrentHealth = MaxHealth;
    }
    public void ApplyDamage(int dmg)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - dmg, 0);
        bloodParticle.Emit(30);
        CheckDead();
    }

    public void Heal(int heal)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + heal, MaxHealth);
    }

    private void CheckDead()
    {
        if(CurrentHealth <= 0)
        {
            myController.Die();
        }
    }
}
