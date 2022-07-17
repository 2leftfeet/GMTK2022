using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRerollState : BaseCombatState
{
    bool diceRolling = false;
    bool skipWhenPossible = false;

    public DiceRerollState(CombatStateMachine stateMachine) : base("DiceReroll", stateMachine) {
    }

    public override void Enter()
    {
        CheckIfRerollAvailable();

        stateMachine.cardSelectionConfirmButton.onClick.AddListener(SkipRerolling);
    }

    void CheckIfRerollAvailable()
    {
        if(stateMachine.playerAgent.rerolls <= 0)
        {
            //if no rerolls, go straight to grouping stage
            ChangeToGroupingState();
        }
    }

    void SkipRerolling()
    {
        if(diceRolling)
        {
            skipWhenPossible = true;
        }
        else
        {
            ChangeToGroupingState();
        }
    }

    void ChangeToGroupingState()
    {
            DiceGroupState diceGroupState = new DiceGroupState(stateMachine, true);
            stateMachine.ChangeState(diceGroupState);
    }

    public override void UpdateLogic()
    {
        if(!diceRolling && stateMachine.playerAgent.rerolls > 0 && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 15f))
            {
                DiceGameObject dice = hit.collider.GetComponent<DiceGameObject>();
                if(dice)
                {
                    dice.transform.position = stateMachine.RandomPointInBounds(stateMachine.playerDiceSpawnBounds.bounds);
                    dice.transform.rotation = Random.rotationUniform;

                    dice.GetComponent<Rigidbody>().velocity = Vector3.forward * 4f;

                    stateMachine.playerAgent.rerolls--;

                    diceRolling = true;
                }
            }
        }
    }

    public override void UpdatePhysics()
    {
        if(diceRolling)
        {
            bool allSleeping = true;

            foreach(CardItem card in stateMachine.activeCards)
            {
                foreach(DiceGameObject dice in card.childDice)
                {
                    if(!dice.GetComponent<Rigidbody>().IsSleeping())
                    {
                        allSleeping = false;
                        break;
                    }
                }
            }

           if(allSleeping)
           {
                Debug.Log("all sleeping");
                diceRolling = false;
                CheckIfRerollAvailable();

                if(skipWhenPossible)
                {
                    ChangeToGroupingState();
                }
           }
        }
    }

    public override void Exit()
    {
        stateMachine.cardSelectionConfirmButton.onClick.RemoveAllListeners();
    }
}
