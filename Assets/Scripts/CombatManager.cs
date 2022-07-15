using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEffects
{
    public int totalDamage = 0;
    public int totalDamageMultiplier = 1;
    public int totalShield = 0;
    public int totalShieldMultiplier = 1;

    public int healthToHeal = 0;

    //not yet sure how we do debuffs and other more unique effects
    //public bool applyWeakness = false;
    public void Reset()
    {
        totalDamage = totalShield = healthToHeal = 0;
        totalDamageMultiplier = totalShieldMultiplier = 1;
    }
}

public class CombatManager : MonoBehaviour
{
    public CombatAgent player;
    public CombatAgent enemy;

    public List<BaseItem> playerSelected;
    public List<BaseItem> enemySelected;

    public UnityEngine.UI.Text debugText;

    RoundEffects playerEffects = new RoundEffects();
    RoundEffects enemyEffects = new RoundEffects();

    void SelectItemPlayer(BaseItem item)
    {
        if(playerSelected.Contains(item))
        {
            Debug.LogError("Selected items already contain " + item.itemName);
            return;
        }
        playerSelected.Add(item);
    }

    void SelectItemEnemy(BaseItem item)
    {
        if(enemySelected.Contains(item))
        {
            Debug.LogError("Selected items already contain " + item.itemName);
            return;
        }
        enemySelected.Add(item);
    }

    void Update()
    {
        debugText.text = $"player hp :{player.health}\nplayer shield :{player.shield}\nenemy hp: {enemy.health}\nenemy shield:{enemy.shield}";
    }

    [ContextMenu("Roll Dice")]
    void RollDice()
    {
        foreach(BaseItem item in playerSelected)
        {
            item.UseItem(playerEffects);
        }

        foreach(BaseItem item in enemySelected)
        {
            item.UseItem(enemyEffects);
        }

        playerSelected.Clear();
        enemySelected.Clear();
    }   

    [ContextMenu("Apply Effects")]
    void ApplyEffects()
    {
        player.health -= enemyEffects.totalDamage * enemyEffects.totalDamageMultiplier;
        player.health += playerEffects.healthToHeal;

        player.shield += playerEffects.totalShield * playerEffects.totalShieldMultiplier;


        enemy.health -= playerEffects.totalDamage * playerEffects.totalDamageMultiplier;
        enemy.health += enemyEffects.healthToHeal;
        
        enemy.shield += enemyEffects.totalShield * enemyEffects.totalShieldMultiplier;

        playerEffects.Reset();
        enemyEffects.Reset();
    }

}
