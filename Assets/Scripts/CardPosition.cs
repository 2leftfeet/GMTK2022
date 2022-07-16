using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour
{
    public Vector3 positionInCardHolder;
    public Quaternion primaryRotation;
    public bool isSelected = false;

    public CardSlot currentSlot;
    public CardSlot oldSlot;

    public CardItem cardItem;

    private void Awake()
    {
        ResetPrimaryPosition(); ;
    }

    private void Start()
    {
        cardItem = GetComponent<CardItem>();
        oldSlot = currentSlot;
    }

    private void Update()
    {
        if (!isSelected) GoBackToCurrentSlot();

        if (Input.GetKey(KeyCode.Space))
        {
            GoBackToOldSlot();
        }
    }

    public void ResetPrimaryPosition()
    {
        positionInCardHolder = transform.position;
        primaryRotation = transform.rotation;
    }

    public void GoBackToCurrentSlot()
    {
        transform.position = Vector3.Slerp(transform.position, currentSlot.cardRestingPosition, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, primaryRotation, 10f * Time.deltaTime);
        currentSlot.isEmpty = false;
    }

    public void GoBackToOldSlot()
    {
        if (currentSlot.isBattleSlot)
        {
            currentSlot.isEmpty = true;
            currentSlot = oldSlot;
        }
    }

    public void AttachToHolderPosition(CardSlot slot, bool isBattleSlot, bool isRewardSlot)
    {
        if (slot != currentSlot && !currentSlot.isBattleSlot && !currentSlot.isRewardSlot) oldSlot = currentSlot;
        if (isBattleSlot) oldSlot.isEmpty = false;
        currentSlot = slot;
    }


}
