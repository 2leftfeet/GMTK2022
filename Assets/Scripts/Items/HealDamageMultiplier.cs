using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Items/Heal Damage Multipler Item")]
public class HealDamageMultiplier : BaseItem
{
    public int damageMultiplierIfHealed = 2;

    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        if(ownerEffects.healthToHeal > 0)
        {
            ownerEffects.totalDamageMultiplier *= damageMultiplierIfHealed;
        }
    }
}
