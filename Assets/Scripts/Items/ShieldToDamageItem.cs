using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Shield Dice To Damage Dice Item", order = 3)]
public class ShieldToDamageItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        ownerEffects.totalDamage += ownerEffects.totalShield;
        ownerEffects.totalDamageMultiplier *= ownerEffects.totalShieldMultiplier;

        ownerEffects.totalShield = 0;
        ownerEffects.totalShieldMultiplier = 1;
    }    
}
