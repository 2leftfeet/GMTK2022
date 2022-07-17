using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Base Item", order = 1)]
public class BaseItem : ScriptableObject
{
    public string itemName;
    public List<DiceSide> diceSides = new List<DiceSide>(6);
    public int diceCount;

    public Sprite image;

    public string textBox;


    public int cooldown = 0;
    public int executionOrder = 0;

    void OnValidate()
    {
        if(diceSides.Count != 6)
        {
            Debug.LogError("Item " + name + " has more or less than six sides on its dice.");
        }
    }

    public virtual void ResolveItem(ref RoundEffects ownerEffects, ref RoundEffects targetEffects, List<DiceGameObject> diceList, CombatAgent owner, CombatAgent target)
    {
        for(int i = 0; i < diceCount; i++)
        {
            DiceSide rolledSide = diceSides[diceList[i].GetSideUp()];

            switch(rolledSide.type)
            {
                case SideType.Damage:
                    ownerEffects.totalDamage += rolledSide.value;
                    break;
                case SideType.DamageMultiplier:
                    ownerEffects.totalDamageMultiplier *= rolledSide.value;
                    break;
                case SideType.Shield:
                    ownerEffects.totalShield += rolledSide.value;
                    break;
                case SideType.ShieldMultiplier:
                    ownerEffects.totalShieldMultiplier *= rolledSide.value;
                    break;
                case SideType.Healing:
                    ownerEffects.healthToHeal += rolledSide.value;
                    break;
            }
        }
    }
}
