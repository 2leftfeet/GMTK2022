using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DiceSideVisuals
{
    public Material material;
    public Texture2D texture;
}

[CreateAssetMenu(fileName = "DiceSideDatabase", menuName = "Databases/DiceSideDatabase", order = 1)]
public class DiceSideDatabase : ScriptableObject
{
    public List<DiceSideVisuals> damage;
    public List<DiceSideVisuals> damageMultiplier;
    public List<DiceSideVisuals> shield;
    public List<DiceSideVisuals> shieldMultiplier;
    public List<DiceSideVisuals> heal;
    public DiceSideVisuals weakness;


    public Material GetMaterial(DiceSide ds)
    {
        switch(ds.type)
        {
            case SideType.Damage:
                return damage[ds.value].material;
            case SideType.DamageMultiplier:
                return damageMultiplier[ds.value].material;
            case SideType.Shield:
                return shield[ds.value].material;
            case SideType.ShieldMultiplier:
                return shieldMultiplier[ds.value].material;
            case SideType.Healing:
                return heal[ds.value].material;
            case SideType.Weakness:
                return weakness.material;
            default:
                Debug.LogError($"No visuals for dice side {ds.type} - {ds.value}");
                return null;
        }
    }

    public Texture2D GetTexture(DiceSide ds)
    {
        switch(ds.type)
        {
            case SideType.Damage:
                return damage[ds.value].texture;
            case SideType.DamageMultiplier:
                return damageMultiplier[ds.value].texture;
            case SideType.Shield:
                return shield[ds.value].texture;
            case SideType.ShieldMultiplier:
                return shieldMultiplier[ds.value].texture;
            case SideType.Healing:
                return heal[ds.value].texture;
            case SideType.Weakness:
                return weakness.texture;
            default:
                Debug.LogError($"No visuals for dice side {ds.type} - {ds.value}");
                return null;
        }
    }
}
