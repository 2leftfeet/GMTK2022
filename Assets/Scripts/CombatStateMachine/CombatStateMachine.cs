using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatStateMachine : MonoBehaviour
{
    BaseCombatState currentState;


    public CombatAgentData TEMP_enemyToSpawn;

    public CardSlot[] playerCardSlots;
    public CardSlot[] enemyCardSlots;

    public CardSlot[] combatSelectSlots;

    public CombatAgent enemyAgent;
    public CombatAgent playerAgent;

    public Button cardSelectionConfirmButton;

    public Collider playerDiceSpawnBounds;
    public DiceGameObject dicePrototype;
    public CardItem cardPrototype;

    public Transform attackDiceGroupPoint;
    public Transform defenseDiceGroupPoint;
    public Transform uniqueDiceGroupPoint;

    public float diceGroupOffset;

    public List<CardItem> activeCards = new List<CardItem>();


    void Start()
    {
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
        return new SpawnEnemyState(this);
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

    public  Vector3 RandomPointInBounds(Bounds bounds) {
    return new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y),
        Random.Range(bounds.min.z, bounds.max.z)
    );
    }
}
