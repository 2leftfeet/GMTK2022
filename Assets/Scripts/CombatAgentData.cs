using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combat Agent", menuName = "Combat Agent", order = 1)]
public class CombatAgentData : ScriptableObject
{
    public int maxHealth = 10;
    
    public List<BaseItem> startingCards = new List<BaseItem>();

    public bool isEnemy = true;

    public int cardsPlayedPerTurn = 2;
}
