using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardsState : BaseCombatState
{
    public SelectCardsState(CombatStateMachine stateMachine) : base("SelectCards", stateMachine) {}

    public List<CardItem> playerSelected;
    public List<CardItem> enemySelected;

    public override void Enter()
    {
        //playerSelected = new List<CardItem>();

        //setup button listener
        stateMachine.cardSelectionConfirmButton.onClick.AddListener(CardsConfirmed);

        //select the cards enemy is about to use
    }

    void CardsConfirmed()
    {
        foreach(var slot in stateMachine.combatSelectSlots)
        {
            //if any of the slots are empty return
            if(slot.currentCard == null)
            {
                return;
            } 
        }

        foreach(var slot in stateMachine.combatSelectSlots)
        {
            stateMachine.activeCards.Add(slot.currentCard);

            slot.currentCard.GetComponent<CardPosition>().GoBackToOldSlot();
        }

        DiceRollSimState diceRollState = new DiceRollSimState(stateMachine);
        stateMachine.ChangeState(diceRollState);
    }

    public override void Exit()
    {
        stateMachine.cardSelectionConfirmButton.onClick.RemoveAllListeners();
    }
}
