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


    List<(CardItem, List<DiceGameObject>)> spawnedDiceByCard = new List<(CardItem, List<DiceGameObject>)>();

    bool diceRolling = false;


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
            foreach((CardItem, List<DiceGameObject>) cardDicePair in spawnedDiceByCard)
            {
                foreach(DiceGameObject dice in cardDicePair.Item2)
                {
                    if(!dice.GetComponent<Rigidbody>().IsSleeping())
                    {
                        allSleeping = false;
                        break;
                    }
                }
                if(!allSleeping) break;
            }

           if(allSleeping)
           {
                diceRolling = false;
                QueryDice();
           }
        }

    }

    [ContextMenu("Spawn Dice")]
    void SpawnDice()
    {
        foreach(CardItem card in playerSelected)
        {
            List<DiceGameObject> childDices = new List<DiceGameObject>();

            int diceCount = card.item.diceCount;
            for(int i = 0; i < diceCount; i++)
            {
                Vector3 randomPosition = RandomPointInBounds(diceSpawnBounds.bounds);
                DiceGameObject spawnedDice = Instantiate(dicePrototype, randomPosition, Random.rotationUniform);
                spawnedDice.parentCard = card;

                spawnedDice.GetComponent<Rigidbody>().velocity = Vector3.forward * 4f;

                childDices.Add(spawnedDice);
            }

            spawnedDiceByCard.Add((card, childDices));
        }
        diceRolling = true;

    }

    void QueryDice()
    {
        foreach((CardItem, List<DiceGameObject>) cardDicePair in spawnedDiceByCard)
            {
                foreach(DiceGameObject dice in cardDicePair.Item2)
                {
                    int rolledIndex = dice.GetSideUp();
                    Debug.Log($"card {cardDicePair.Item1.name} of item {cardDicePair.Item1.item} rolled {cardDicePair.Item1.item.diceSides[rolledIndex].type}-{cardDicePair.Item1.item.diceSides[rolledIndex].value}");
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
