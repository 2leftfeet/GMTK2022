using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Vampiric Item", order = 2)]
public class VampiricItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        ownerEffects.healthToHeal += ownerEffects.totalDamage * ownerEffects.totalDamageMultiplier + ownerEffects.unscaledDamage;
    }    
}
