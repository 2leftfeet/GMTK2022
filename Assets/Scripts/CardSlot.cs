using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool isEmpty;
    public bool isEnemy;
    [SerializeField] public bool isBattleSlot;
    [SerializeField] public bool isRewardSlot;

    CardPosition cardPosition;

    public CardItem currentCard;

    public Vector3 cardRestingPosition;
    private void Awake()
    {
        ResetPrimaryPosition();
    }

    private void Start()
    {
        CheckHolder();
    }

    private void Update()
    {
        ResetPrimaryPosition();
    }

    public void ResetPrimaryPosition()
    {
        cardRestingPosition = transform.position + (Vector3.up * 0.1f);
    }

    public void AttachCard(CardItem card)
    {
        isEmpty = false;
        currentCard = card;

        card.GetComponent<CardPosition>().currentSlot = this;
    }

    public void RemoveCard()
    {
        isEmpty = true;
        currentCard = null;
    }

    public void CheckHolder()
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("Card");
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, layer_mask))
        {
            CardItem cardItem = hit.collider.GetComponent<CardItem>();
            if (cardItem)
            {
                cardPosition = cardItem.GetComponent<CardPosition>();
                cardPosition.cardItem = cardItem;
                CardSlot thisCardSlot = this.gameObject.GetComponent<CardSlot>();
                cardPosition.currentSlot = thisCardSlot;
                thisCardSlot.isEmpty = false;
                thisCardSlot.currentCard = cardItem;
                if (cardPosition.currentSlot.isRewardSlot) cardPosition.cardItem.isRewardCard = true;
            }
        }
    }

}
