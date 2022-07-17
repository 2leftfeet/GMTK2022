using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/White Flag Item")]
public class WhiteFlagItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        ownerEffects.totalDamage = 0;
        ownerEffects.totalDamageMultiplier = 1;
        ownerEffects.unscaledDamage = 0;

        targetEffects.totalDamage = 0;
        targetEffects.totalDamageMultiplier = 1;
        targetEffects.unscaledDamage = 0;
    }
}
