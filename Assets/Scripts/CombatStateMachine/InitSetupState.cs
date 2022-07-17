using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSetupState : BaseCombatState
{
    public InitSetupState(CombatStateMachine stateMachine) : base("InitSetup", stateMachine){}

    public override void Enter()
    {
        CombatAgentData playerData = stateMachine.playerData;

        if(stateMachine.playerAgent != null)
        {
            Debug.LogError("player agent not null when creating player");
        }

        stateMachine.playerAgent = stateMachine.gameObject.AddComponent(typeof(CombatAgent)) as CombatAgent;
        stateMachine.playerAgent.maxHealth = playerData.maxHealth;
        
        for(int i = 0; i < playerData.startingCards.Count; i++)
        {
            CardSlot slot = stateMachine.playerCardSlots[i];

            CardItem spawnedCard = stateMachine.SpawnCard(slot.cardRestingPosition, Quaternion.Euler(-90f, 0f, 0f));
            slot.AttachCard(spawnedCard);

            spawnedCard.item = playerData.startingCards[i];
            spawnedCard.owner = stateMachine.playerAgent;

            stateMachine.playerAgent.inventory.Add(spawnedCard);
        }

        stateMachine.playerAgent.Setup();
        stateMachine.UpdatePlayerUI();

        SpawnEnemyState spawnEnemyState = new SpawnEnemyState(stateMachine);
        stateMachine.ChangeState(spawnEnemyState);
    }
}
