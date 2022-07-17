using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Pighead Item")]
public class PigHeadItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        int tempDamage = ownerEffects.totalDamage;
        int tempDamageMult = ownerEffects.totalDamageMultiplier;
        int tempUnscaledDamage = ownerEffects.unscaledDamage;

        ownerEffects.totalDamage = targetEffects.totalDamage;
        ownerEffects.totalDamageMultiplier = targetEffects.totalDamageMultiplier;
        ownerEffects.unscaledDamage = targetEffects.unscaledDamage;

        targetEffects.totalDamage = tempDamage;
        targetEffects.totalDamageMultiplier = tempDamageMult;
        targetEffects.unscaledDamage = tempUnscaledDamage;
    }
}
