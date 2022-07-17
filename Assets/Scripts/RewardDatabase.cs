using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DiceSideDatabase", menuName = "Databases/RewardDatabase", order = 3)]
public class RewardDatabase : ScriptableObject
{
    public List<BaseItem> tierOneRewards;
    public List<BaseItem> tierTwoRewards;
    public List<BaseItem> tierThreeRewards;
}
