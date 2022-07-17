using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatStateMachine : MonoBehaviour
{
    BaseCombatState currentState;


    public EnemyDatabase enemyDatabase;
    public RewardDatabase rewardDatabase;
    public CombatAgentData playerData;

    public CardSlot[] playerCardSlots;
    public CardSlot[] enemyCardSlots;

    public CardSlot[] combatSelectSlots;

    public CombatAgent enemyAgent;
    public CombatAgent playerAgent;

    public Button cardSelectionConfirmButton;

    public Collider playerDiceSpawnBounds;
    public Collider enemyDiceSpawnBounds;
    public DiceGameObject dicePrototype;
    public CardItem cardPrototype;

    public Transform attackDiceGroupPoint;
    public Transform defenseDiceGroupPoint;
    public Transform uniqueDiceGroupPoint;

    public Transform enemyAttackDiceGroupPoint;
    public Transform enemyDefenseDiceGroupPoint;
    public Transform enemyUniqueDiceGroupPoint;

    public TextMeshProUGUI playerHealthVial;
    public TextMeshProUGUI enemyHealthVial;
    public TextMeshProUGUI playerShield;
    public TextMeshProUGUI enemyShield;
    public TextMeshProUGUI playerRerolls;

    public TextMeshProUGUI tutorialText;

    public RewardTable rewardTable;
    public MenuTable menuTable;

    public InvokeButton coinButton;
    
    

    public float diceGroupOffset;

    public List<CardItem> activeCards = new List<CardItem>();

    public List<CardItem> enemyActiveCards = new List<CardItem>();

    [HideInInspector]
    public int currentRound = 1;


    void Start()
    {
        currentRound = 1;

        foreach(var slot in combatSelectSlots)
        {
            if(!slot.isBattleSlot) Debug.LogWarning($"slot {slot.name} should be a battle slot.");
        }

        currentState = GetInitialState();
        if(currentState != null)
        {
            currentState.Enter();
        }
    }

    void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    void FixedUpdate()
    {
        if(currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(BaseCombatState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseCombatState GetInitialState()
    {
        return new InitSetupState(this);
        //return new RewardsPhaseState(this);
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }

    public DiceGameObject SpawnDice(Vector3 position, Quaternion rotation)
    {
        return Instantiate(dicePrototype, position, rotation);
    }

    public CardItem SpawnCard(Vector3 position, Quaternion rotation)
    {
        return Instantiate(cardPrototype, position, rotation);
    }

    public void ClearDice()
    {
        foreach(CardItem card in activeCards)
        {
            foreach(DiceGameObject dice in card.childDice)
            {
                Destroy(dice.gameObject);
            }
            card.childDice.Clear();
        }

        foreach(CardItem card in enemyActiveCards)
        {
            foreach(DiceGameObject dice in card.childDice)
            {
                Destroy(dice.gameObject);
            }
            card.childDice.Clear();
        }
    }

    public void ClearActiveCards()
    {
        foreach(CardItem card in playerAgent.inventory)
        {
            card.CooldownDecrement();
        }

        //set active cards on cooldown
        foreach(CardItem card in activeCards)
        {
            card.SetOnCooldown();
        }

        activeCards.Clear();
        enemyActiveCards.Clear();
    }

    public void UpdatePlayerUI()
    {
        playerHealthVial.text = playerAgent.health.ToString();
        playerShield.text = playerAgent.shield.ToString();
    }

    public void UpdateEnemyUI()
    {
        enemyHealthVial.text = enemyAgent.health.ToString();
        enemyShield.text = enemyAgent.shield.ToString();
    }

    public void UpdateRerollUI()
    {
        playerRerolls.text = playerAgent.rerolls.ToString();
    }

    public void CleanupEnemy()
    {
        foreach(CardItem card in enemyAgent.inventory)
        {
            Destroy(card.gameObject);
        }

        foreach(CardSlot slot in enemyCardSlots)
        {
            slot.RemoveCard();
        }


        Destroy(enemyAgent);
        enemyAgent = null;
    }

    public void CleanupPlayer()
    {
        foreach(CardItem card in playerAgent.inventory)
        {
            Destroy(card.gameObject);
        }

        foreach(CardSlot slot in playerCardSlots)
        {
            slot.RemoveCard();
        }


        Destroy(playerAgent);
        playerAgent = null;
    }

    public void CleanupRewards()
    {
        foreach(CardSlot slot in rewardTable.cardSlots)
        {
            if(slot.currentCard != null)
             Destroy(slot.currentCard.gameObject);
            slot.RemoveCard();
        }
    }

    public  Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
    }
}
