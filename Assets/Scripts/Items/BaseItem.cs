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

    public int executionOrder = 0;

    void OnValidate()
    {
        if(diceSides.Count != 6)
        {
            Debug.LogError("Item " + name + " has more or less than six sides on its dice.");
        }
    }

    public virtual void ResolveItem(ref RoundEffects effects, List<DiceGameObject> diceList)
    {
        for(int i = 0; i < diceCount; i++)
        {
            DiceSide rolledSide = diceSides[diceList[i].GetSideUp()];

            switch(rolledSide.type)
            {
                case SideType.Damage:
                    effects.totalDamage += rolledSide.value;
                    break;
                case SideType.DamageMultiplier:
                    effects.totalDamageMultiplier *= rolledSide.value;
                    break;
                case SideType.Shield:
                    effects.totalShield += rolledSide.value;
                    break;
                case SideType.ShieldMultiplier:
                    effects.totalShieldMultiplier *= rolledSide.value;
                    break;
                case SideType.Healing:
                    effects.healthToHeal += rolledSide.value;
                    break;
            }
        }
    }
}
