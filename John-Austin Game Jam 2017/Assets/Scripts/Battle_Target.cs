using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battle_Target : MonoBehaviour {

    private float m_Health;
    private float m_MaxHealth;

    public bool is_dead = false;
    public Battle_Manager m_Manager;

    public GameObject Attack1;
    public GameObject Attack2;
    public GameObject Attack3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetHealth(float health)
    {
        m_Health = health;
        m_MaxHealth = health;
    }

    public virtual void Attack(Battle_Target Target, float damage)
    {
        Target.Take_Damage(damage);
    }

    public void Take_Damage(float damage)
    {
        m_Health -= damage;

        if(m_Health <= 0)
        {
            Die();
        }

        if(m_Health > m_MaxHealth)
        {
            m_Health = m_MaxHealth;
        }
    }

    public float GetHealth()
    {
        return m_Health;
    }

    public float GetMaxHealth()
    {
        return m_MaxHealth;
    }

    public abstract void Die();

    public virtual void reload()
    {
        m_Health = m_MaxHealth;
        is_dead = false;
    }

}
