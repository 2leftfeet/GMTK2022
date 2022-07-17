using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Shield Dice To Damage Dice Item", order = 3)]
public class ShieldToDamageItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects effects, List<DiceGameObject> diceList, CombatAgent owner)
    {
        base.ResolveItem(ref effects, diceList, owner);

        effects.totalDamage += effects.totalShield;
        effects.totalDamageMultiplier *= effects.totalShieldMultiplier;

        effects.totalShield = 0;
        effects.totalShieldMultiplier = 1;
    }    
}

[CreateAssetMenu(fileName = "Item", menuName = "Items/Shield Amount To Damage Item", order = 4)]
public class AttackShieldItem : BaseItem
{
    public override void ResolveItem(ref RoundEffects effects, List<DiceGameObject> diceList, CombatAgent owner)
    {
        base.ResolveItem(ref effects, diceList, owner);

        effects.unscaledDamage = owner.GetShield();
    }
}
