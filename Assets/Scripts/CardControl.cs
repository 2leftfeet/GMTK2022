using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, -2f);
    [SerializeField] float yValue = 5f;

    CardItem controlledCard;

    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (plane.Raycast(ray, out distance))
            {
                worldPosition = ray.GetPoint(distance);
            }
            if (!controlledCard && Physics.Raycast(ray, out hit))
            {
                CardItem hitCard = hit.collider.GetComponent<CardItem>();
                if (hitCard)
                {
                    if (hitCard.GetComponent<CardPosition>().isMovable)
                    {
                        controlledCard = hitCard;
                        controlledCard.GetComponent<CardPosition>().isSelected = true;
                        controlledCard.GetComponent<CardPosition>().currentSlot.isEmpty = true;
                    }
                }
            }
        }

        if (controlledCard)
        {
            controlledCard.transform.position = Vector3.Lerp(controlledCard.transform.position, worldPosition, 10f * Time.deltaTime);
            controlledCard.transform.LookAt(worldPosition + Vector3.up * yValue, -Vector3.forward);
        }

        if (Input.GetMouseButtonUp(0))
        {
            CardPlacementLogic();
            if (controlledCard) controlledCard.GetComponent<CardPosition>().isSelected = false;
            controlledCard = null;
        }
    }

    public void DestroyCard(CardItem currentCard)
    {
        Destroy(currentCard.gameObject);
    }

    void CardPlacementLogic()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("CardHolder");

        if (controlledCard && Physics.Raycast(worldPosition, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layer_mask))
        {
            CardSlot hitCardSlot = hit.collider.GetComponent<CardSlot>();
            if (hitCardSlot)
            {
                if(hitCardSlot.isEnemy) return;


                CardItem cardItem = controlledCard.GetComponent<CardItem>();
                CardPosition cardPosition = controlledCard.GetComponent<CardPosition>();
                if (cardItem.isRewardCard)
                {
                    if (!hitCardSlot.isEmpty) DestroyCard(hitCardSlot.currentCard);
                    hitCardSlot.AttachCard(controlledCard);
                    cardPosition.positionInCardHolder = hitCardSlot.cardRestingPosition;
                    cardPosition.AttachToHolderPosition(hitCardSlot, hitCardSlot.isBattleSlot, hitCardSlot.isRewardSlot);
                    cardItem.isRewardCard = false;
                }
                else if (hitCardSlot.isEmpty && !hitCardSlot.isRewardSlot)
                {
                    cardPosition.positionInCardHolder = hitCardSlot.cardRestingPosition;

                    if(cardPosition.currentSlot.isBattleSlot)
                    {
                        cardPosition.currentSlot.RemoveCard();
                    }
                    hitCardSlot.AttachCard(controlledCard);
                    cardPosition.AttachToHolderPosition(hitCardSlot, hitCardSlot.isBattleSlot, hitCardSlot.isRewardSlot);
                }
                if (hitCardSlot.isRewardSlot)
                {
                    return;
                }
                else if (cardPosition.oldSlot == hitCardSlot && !cardItem.isRewardCard)
                {
                    if(cardPosition.currentSlot.isBattleSlot)
                    {
                        cardPosition.currentSlot.RemoveCard();
                    }

                    controlledCard.GetComponent<CardPosition>().positionInCardHolder = hitCardSlot.cardRestingPosition;
                    hitCardSlot.AttachCard(controlledCard);
                    controlledCard.GetComponent<CardPosition>().AttachToHolderPosition(hitCardSlot, hitCardSlot.isBattleSlot, hitCardSlot.isRewardSlot);
                }

            }
        }
    }
}
