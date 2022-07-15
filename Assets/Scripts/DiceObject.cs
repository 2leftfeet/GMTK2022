using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SideType
{
    Damage,
    DamageMultiplier,
    Shield,
    ShieldMultiplier,
    Healing,
    Weakness,
    Number
}

[System.Serializable]
public class DiceSide
{
    public SideType type;
    public int value;
}