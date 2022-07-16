using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool isEmpty;
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
        cardRestingPosition = transform.position + (Vector3.up * 0.03f);
    }

    public void AttachCard(CardItem card)
    {
        isEmpty = false;
        currentCard = card;
    }

    public void RemoveCard()
    {
        isEmpty = true;
    }

    public void CheckHolder()
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("Card");
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, layer_mask))
        {
            Debug.Log("Hit");
            CardItem cardItem = hit.collider.GetComponent<CardItem>();
            if (cardItem)
            {
                Debug.Log("Hit Card Item");
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
