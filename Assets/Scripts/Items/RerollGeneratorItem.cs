using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/RerollGenerator", order = 4)]
public class RerollGenerator : BaseItem
{
    public override void ResolveItem(ref RoundEffects effects, List<DiceGameObject> diceList, CombatAgent owner)
    {
        base.ResolveItem(ref effects, diceList, owner);

        if(effects.totalDamage > 0 || effects.unscaledDamage > 0)
        {
            owner.rerolls++;
        }
    }    
}


