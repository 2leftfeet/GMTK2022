using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyState : BaseCombatState
{
    public SpawnEnemyState(CombatStateMachine stateMachine) : base("SpawnEnemy", stateMachine)
    {}

    public override void Enter()
    {
        //later should pick out enemy from EnemyDatabase based on which round it is
        //for now pick from temp variable
        CombatAgentData enemyToSpawn = stateMachine.TEMP_enemyToSpawn;

        if(stateMachine.enemyAgent != null)
        {
            Debug.LogError("enemy agent not properly destroyed before creating a new one");
        }


        stateMachine.enemyAgent = stateMachine.gameObject.AddComponent(typeof(CombatAgent)) as CombatAgent;
        stateMachine.enemyAgent.maxHealth = enemyToSpawn.maxHealth;
        stateMachine.enemyAgent.cardPlayedPerTurn = enemyToSpawn.cardsPlayedPerTurn;

        for(int i = 0; i < enemyToSpawn.startingCards.Count; i++)
        {
            CardSlot slot = stateMachine.enemyCardSlots[i];

            CardItem spawnedCard = stateMachine.SpawnCard(slot.cardRestingPosition, Quaternion.Euler(-90f,0f,0f));
            slot.AttachCard(spawnedCard);

            spawnedCard.item = enemyToSpawn.startingCards[i];
            spawnedCard.owner = stateMachine.enemyAgent;

            spawnedCard.GetComponent<CardPosition>().isMovable = false;
            spawnedCard.GetComponent<CardHighlight>().isEnemy = true;

            stateMachine.enemyAgent.inventory.Add(spawnedCard);
            
        }

        SelectCardsState selectCardsState = new SelectCardsState(stateMachine);
        stateMachine.ChangeState(selectCardsState);
    }
}
