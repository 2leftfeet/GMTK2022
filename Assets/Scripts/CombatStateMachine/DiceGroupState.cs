using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceGroupState : BaseCombatState
{
    static float groupStateDuration = 3f;
    float timer = 0.0f;

    bool isPlayer;
    List<CardItem> cardsToGroup;

    Transform attackGroup;
    Transform defenseGroup;
    Transform uniqueGroup;

    Vector3 offsetDir;

    public DiceGroupState(CombatStateMachine stateMachine, bool isPlayer) : base("DiceGrouping", stateMachine)
    {
        this.isPlayer = isPlayer;
    }

    public override void Enter()
    {
        cardsToGroup = isPlayer ? stateMachine.activeCards : stateMachine.enemyActiveCards;

        attackGroup = isPlayer ? stateMachine.attackDiceGroupPoint : stateMachine.enemyAttackDiceGroupPoint;
        defenseGroup = isPlayer ? stateMachine.defenseDiceGroupPoint : stateMachine.enemyDefenseDiceGroupPoint;
        uniqueGroup = isPlayer ? stateMachine.uniqueDiceGroupPoint : stateMachine.enemyUniqueDiceGroupPoint;

        offsetDir = isPlayer ? Vector3.forward : Vector3.back;

        Dictionary<SideType, List<DiceGameObject>> groupedDice = new Dictionary<SideType, List<DiceGameObject>>();
        
        foreach(CardItem card in cardsToGroup)
        {
            foreach(DiceGameObject dice in card.childDice)
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
                    dice.MoveToOverview(attackGroup.position + offsetDir * stateMachine.diceGroupOffset * attackCount);
                    attackCount++;
                }
            }
            else if(pair.Key == SideType.Shield || pair.Key == SideType.ShieldMultiplier)
            {
                foreach(var dice in pair.Value)
                {
                    dice.MoveToOverview(defenseGroup.position + offsetDir * stateMachine.diceGroupOffset * defenseCount);
                    defenseCount++;
                }
            }
            else if(pair.Key == SideType.Healing)
            {
                foreach(var dice in pair.Value)
                {
                    dice.MoveToOverview(uniqueGroup.position + offsetDir * stateMachine.diceGroupOffset * uniqueCount);
                    uniqueCount++;
                }
            }
            }
    }

    public override void UpdateLogic()
    {
        timer += Time.deltaTime;
        if(timer > groupStateDuration)
        {
            if(isPlayer)
            {
                EnemyMoveCardsState enemyMoveCardsState = new EnemyMoveCardsState(stateMachine);
                stateMachine.ChangeState(enemyMoveCardsState);
            }
            else
            {
                ResolveEffectsState resolveState = new ResolveEffectsState(stateMachine);
                stateMachine.ChangeState(resolveState);
            }
        }
    }
}
