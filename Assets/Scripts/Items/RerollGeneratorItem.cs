using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/RerollGenerator", order = 4)]
public class RerollGeneratorItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        if(ownerEffects.totalDamage > 0 || ownerEffects.unscaledDamage > 0)
        {
            owner.rerolls++;
        }
    }    
}


