using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RewardsPhaseState : BaseCombatState
{
    public RewardsPhaseState(CombatStateMachine stateMachine) : base("RewardsPhase", stateMachine) {}

    List<CardItem> spawnedCards = new List<CardItem>();

    public override void Enter()
    {
        stateMachine.rewardTable.isCalled = true;

        //after round 1 - tier 1
        //after round 2 - tier 2
        //after round 3 - tier 3
        //after round 4 - tier 3
        List<BaseItem> rewardList;

        if(stateMachine.currentRound == 1)
        {
            rewardList = stateMachine.rewardDatabase.tierOneRewards;
        }
        else if(stateMachine.currentRound == 2)
        {
            rewardList = stateMachine.rewardDatabase.tierTwoRewards;
        }
        else
        {
            rewardList = stateMachine.rewardDatabase.tierThreeRewards;
        }

        var randomItems = rewardList.OrderBy(x => Random.value).Take(3).ToList();

        for(int i = 0; i < randomItems.Count; i++)
        {
            CardSlot slot = stateMachine.rewardTable.cardSlots[i];

            CardItem spawnedCard = stateMachine.SpawnCard(slot.cardRestingPosition, Quaternion.Euler(-90f, 0f, 0f));
            slot.AttachCard(spawnedCard);

            spawnedCard.isRewardCard = true;

            spawnedCard.item = randomItems[i];

            spawnedCards.Add(spawnedCard);
        }

    }

    public override void UpdateLogic()
    {
        bool endRewards = false;
        foreach(CardItem card in spawnedCards)
        {
            CardPosition cardPosition = card.GetComponent<CardPosition>();
            //check if any card is now in player sloats
            CardSlot slot = cardPosition.currentSlot;
            if(stateMachine.playerCardSlots.Contains(slot))
            {
                card.owner = stateMachine.playerAgent;
                stateMachine.playerAgent.inventory.Add(card);

                cardPosition.oldSlot.RemoveCard();

                cardPosition.oldSlot = cardPosition.currentSlot;

                //remove rewarded card from spawnedcards
                endRewards = true;
                break;
            }
        }

        if(endRewards)
        {
            //create round cleanup state and assign it
            RoundCleanupState cleanupState = new RoundCleanupState(stateMachine);
            stateMachine.ChangeState(cleanupState);
        }
    }


    public override void Exit()
    {
        stateMachine.rewardTable.isCalled = false;
    }
}
