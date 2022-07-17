using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRollSimState : BaseCombatState
{
    static float maxTime = 5f;
    float timer = 0f;

    bool isPlayer;
    List<CardItem> cardsToRoll;
    Bounds diceSpawnBounds;
    Vector3 velocityDir;

    public DiceRollSimState(CombatStateMachine stateMachine, bool isPlayer) : base("DiceRollSim", stateMachine) {
        this.isPlayer = isPlayer;
    }

    bool diceRolling = false;

    public override void Enter()
    {   
        cardsToRoll = isPlayer ? stateMachine.activeCards : stateMachine.enemyActiveCards;
        diceSpawnBounds = isPlayer ? stateMachine.playerDiceSpawnBounds.bounds : stateMachine.enemyDiceSpawnBounds.bounds;
        velocityDir = isPlayer ? Vector3.forward : Vector3.back;

        //Spawn and launch player dice
        foreach(CardItem card in cardsToRoll)
        {
            int diceCount = card.item.diceCount;
            for(int i = 0; i < diceCount; i++)
            {
                Vector3 randomPosition = stateMachine.RandomPointInBounds(diceSpawnBounds);
                DiceGameObject newDice = stateMachine.SpawnDice(randomPosition, Random.rotationUniform);
                newDice.parentCard = card;


                newDice.GetComponent<Rigidbody>().velocity = velocityDir * 4f;

                card.childDice.Add(newDice);
            }
        }

        diceRolling = true;

    }

    public override void UpdatePhysics()
    {
        timer += Time.deltaTime;
        if(timer > maxTime)
        {
            if(isPlayer)
                {
                    DiceRerollState rerollState = new DiceRerollState(stateMachine);
                    stateMachine.ChangeState(rerollState);
                }
                else
                {
                    DiceGroupState groupState = new DiceGroupState(stateMachine, false);
                    stateMachine.ChangeState(groupState);
                }
        }

        if(diceRolling)
        {
            bool allSleeping = true;

            foreach(CardItem card in cardsToRoll)
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
                if(isPlayer)
                {
                    DiceRerollState rerollState = new DiceRerollState(stateMachine);
                    stateMachine.ChangeState(rerollState);
                }
                else
                {
                    DiceGroupState groupState = new DiceGroupState(stateMachine, false);
                    stateMachine.ChangeState(groupState);
                }
           }
        }
    }

    
}
