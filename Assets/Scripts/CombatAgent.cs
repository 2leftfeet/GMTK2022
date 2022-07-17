using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAgent : MonoBehaviour
{
    public int maxHealth = 50;
    public int cardPlayedPerTurn = 2;

    public int health = 13;
    public int shield;

    public int rerolls = 3;

    public List<CardItem> inventory = new List<CardItem>();

    public Texture2D portrait;

    public void Setup()
    {
        health = maxHealth;
        shield = 0;
    }

    public void DealDamage(int amount)
    {
        int damageLeft = amount;

        if(shield > 0)
        {
            int damageToShield = Mathf.Min(shield, damageLeft);
            shield -= damageToShield;

            damageLeft -= damageToShield;
        }

        health -= damageLeft;
    }

    public void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void AddShield(int amount)
    {
        shield += amount;      
    }

    public int GetShield() { return shield;}
}
