using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceSideDatabase", menuName = "Databases/EnemyDatabase", order = 2)]
public class EnemyDatabase : ScriptableObject
{
    public List<CombatAgentData> enemies;
}
