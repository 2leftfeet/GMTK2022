using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Vampiric Item", order = 2)]
public class VampiricItem : BaseItem
{
    public override void UseItem(RoundEffects effects)
    {
        for(int i = 0; i < diceCount; i++)
        {
            //choose random side
            int randomSideIndex = Random.Range(0, 6);
            DiceSide randomSide = diceSides[randomSideIndex];

            Debug.Log("Item " + itemName + " rolled " + randomSide.type + " " + randomSide.value);

            switch(randomSide.type)
            {
                case SideType.Damage:
                    effects.totalDamage += randomSide.value;
                    effects.healthToHeal += randomSide.value;
                    break;
                case SideType.DamageMultiplier:
                    effects.totalDamageMultiplier *= randomSide.value;
                    break;
                case SideType.Shield:
                    effects.totalShield += randomSide.value;
                    break;
                case SideType.ShieldMultiplier:
                    effects.totalShieldMultiplier *= randomSide.value;
                    break;
                case SideType.Healing:
                    effects.healthToHeal += randomSide.value;
                    break;
            }
        }
    }
}
