using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Baser Item", order = 1)]
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
}
