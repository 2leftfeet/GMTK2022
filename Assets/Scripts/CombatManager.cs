using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEffects
{
    public int totalDamage = 0;
    public int totalDamageMultiplier = 1;
    public int totalShield = 0;
    public int totalShieldMultiplier = 1;

    public int healthToHeal = 0;

    //not yet sure how we do debuffs and other more unique effects
    //public bool applyWeakness = false;
    public void Reset()
    {
        totalDamage = totalShield = healthToHeal = 0;
        totalDamageMultiplier = totalShieldMultiplier = 1;
    }
}

public class CombatManager : MonoBehaviour
{
    public CombatAgent player;
    public CombatAgent enemy;

    public List<CardItem> playerSelected;
    public List<CardItem> enemySelected;

    public DiceGameObject dicePrototype;
    public Collider diceSpawnBounds;

    RoundEffects playerEffects = new RoundEffects();
    RoundEffects enemyEffects = new RoundEffects();


    List<DiceGameObject> diceList = new List<DiceGameObject>();

    bool diceRolling = false;

    public Transform attackDiceLineStart;
    public Transform defenseDiceLineStart;
    public Transform uniqueDiceLineStart;
    public float diceOverviewOffset = 0.3f;


    void SelectItemPlayer(CardItem item)
    {
        if(playerSelected.Contains(item))
        {
            Debug.LogError("Selected cards already contain " + item.name);
            return;
        }
        playerSelected.Add(item);
    }

    void SelectItemEnemy(CardItem item)
    {
        if(enemySelected.Contains(item))
        {
            Debug.LogError("Selected cards already contain " + item.name);
            return;
        }
        enemySelected.Add(item);
    }

    void Update()
    {
        if(diceRolling)
        {
            bool allSleeping = true;

            foreach(DiceGameObject dice in diceList)
            {
                if(!dice.GetComponent<Rigidbody>().IsSleeping())
                {
                    allSleeping = false;
                    break;
                }
            }

           if(allSleeping)
           {
                diceRolling = false;
                QueryDice();
           }
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            SpawnDice();
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            GroupDice();
        }

    }

    [ContextMenu("Spawn Dice")]
    void SpawnDice()
    {
        foreach(CardItem card in playerSelected)
        {

            int diceCount = card.item.diceCount;
            for(int i = 0; i < diceCount; i++)
            {
                Vector3 randomPosition = RandomPointInBounds(diceSpawnBounds.bounds);
                DiceGameObject spawnedDice = Instantiate(dicePrototype, randomPosition, Random.rotationUniform);
                spawnedDice.parentCard = card;

                spawnedDice.GetComponent<Rigidbody>().velocity = Vector3.forward * 4f;

                diceList.Add(spawnedDice);
            }
        }
        diceRolling = true;

    }

    void QueryDice()
    {
        
        foreach(DiceGameObject dice in diceList)
        {
            int rolledIndex = dice.GetSideUp();
            Debug.Log($"card {dice.parentCard.name} of item {dice.parentCard.item} rolled {dice.parentCard.item.diceSides[rolledIndex].type}-{dice.parentCard.item.diceSides[rolledIndex].value}");
        }
            
    }

    [ContextMenu("Group Dice")]
    void GroupDice()
    {
        Dictionary<SideType, List<DiceGameObject>> groupedDice = new Dictionary<SideType, List<DiceGameObject>>();
        
        foreach(DiceGameObject dice in diceList)
        {
            SideType diceType = dice.parentCard.item.diceSides[dice.GetSideUp()].type;

            if(!groupedDice.ContainsKey(diceType))
            {
                groupedDice.Add(diceType, new List<DiceGameObject>{dice});
            }
            else
            {
                groupedDice[diceType].Add(dice);
            }
        }

        int attackCount = 0;
        int defenseCount = 0;
        int uniqueCount = 0;

        foreach(var pair in groupedDice)
        {
            if(pair.Key == SideType.Damage || pair.Key == SideType.DamageMultiplier)
            {
                foreach(var dice in pair.Value)
                {
                    dice.MoveToOverview(attackDiceLineStart.position + Vector3.forward * diceOverviewOffset * attackCount);
                    attackCount++;
                }
            }
            else if(pair.Key == SideType.Shield || pair.Key == SideType.ShieldMultiplier || pair.Key == SideType.Healing)
            {
                foreach(var dice in pair.Value)
                {
                    dice.MoveToOverview(defenseDiceLineStart.position + Vector3.forward * diceOverviewOffset * defenseCount);
                    defenseCount++;
                }
            }
            else
            {
                foreach(var dice in pair.Value)
                {
                    dice.MoveToOverview(uniqueDiceLineStart.position + Vector3.forward * diceOverviewOffset * uniqueCount);
                    uniqueCount++;
                }
            }
            }
    }
        
    



    [ContextMenu("Roll Dice")]
    void RollDice()
    {
        
    }   

    [ContextMenu("Apply Effects")]
    void ApplyEffects()
    {
        player.health -= enemyEffects.totalDamage * enemyEffects.totalDamageMultiplier;
        player.health += playerEffects.healthToHeal;

        player.shield += playerEffects.totalShield * playerEffects.totalShieldMultiplier;


        enemy.health -= playerEffects.totalDamage * playerEffects.totalDamageMultiplier;
        enemy.health += enemyEffects.healthToHeal;
        
        enemy.shield += enemyEffects.totalShield * enemyEffects.totalShieldMultiplier;

        playerEffects.Reset();
        enemyEffects.Reset();
    }

    public  Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
}

}
