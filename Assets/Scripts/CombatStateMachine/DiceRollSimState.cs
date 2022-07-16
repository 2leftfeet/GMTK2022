using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollSimState : BaseCombatState
{
    public DiceRollSimState(CombatStateMachine stateMachine) : base("DiceRollSim", stateMachine) {
    }

    bool diceRolling = false;

    public override void Enter()
    {   
        //Spawn and launch player dice
        foreach(CardItem card in stateMachine.activeCards)
        {
            int diceCount = card.item.diceCount;
            for(int i = 0; i < diceCount; i++)
            {
                Vector3 randomPosition = stateMachine.RandomPointInBounds(stateMachine.playerDiceSpawnBounds.bounds);
                DiceGameObject newDice = stateMachine.SpawnDice(randomPosition, Random.rotationUniform);
                newDice.parentCard = card;


                newDice.GetComponent<Rigidbody>().velocity = Vector3.forward * 4f;

                card.childDice.Add(newDice);
            }
        }

        diceRolling = true;

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
                diceRolling = false;
                //goto dice reroll
                DiceRerollState rerollState = new DiceRerollState(stateMachine);
                stateMachine.ChangeState(rerollState);
           }
        }
    }

    
}
