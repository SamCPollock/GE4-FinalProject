using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    [SerializeField]
    public  float currentHealth;
    public float CurrentHealth => currentHealth;

    [SerializeField]
    private float maxHealth;
    public float MaxHealth => maxHealth;


    protected virtual void Start()
    {
        currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage; 
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
