using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Garlic Item")]
public class GarlicItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        base.ResolveItem(ref ownerEffects, ref targetEffects, diceList, owner, target);

        ownerEffects.unscaledDamage += ownerEffects.healthToHeal;
    }
}
