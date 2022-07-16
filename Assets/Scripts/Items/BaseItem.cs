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

    void OnValidate()
    {
        if(diceSides.Count != 6)
        {
            Debug.LogError("Item " + name + " has more or less than six sides on its dice.");
        }
    }

    public virtual void UseItem(RoundEffects effects)
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
