using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Vampiric Item", order = 2)]
public class VampiricItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects effects, List<DiceGameObject> diceList)
    {
        base.ResolveItem(ref effects, diceList);

        effects.healthToHeal += effects.totalDamage * effects.totalDamageMultiplier;
    }    
}
