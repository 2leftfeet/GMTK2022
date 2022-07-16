using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool isEmpty;


    public Vector3 primaryPosition;
    private void Awake()
    {
        ResetPrimaryPosition();
    }

    public void ResetPrimaryPosition()
    {
        primaryPosition = transform.position + (Vector3.up * 0.03f);
    }
    
    public void AttachCard()
    {
        isEmpty = false;
    }

    public void RemoveCard()
    {
        isEmpty = true;
    }
}
