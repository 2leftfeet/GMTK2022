using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryPosition : MonoBehaviour
{
    public Vector3 primaryPosition;
    public Quaternion primaryRotation;
    public bool isSelected = false;

    public CardSlot currentSlot;

    private void Awake()
    {
        ResetPrimaryPosition();;
    }

    private void Start()
    {
        CheckHolder();
    }

    private void Update()
    {
        if (!isSelected) GoBack();
    }

    public void ResetPrimaryPosition()
    {
        primaryPosition = transform.position;
        primaryRotation = transform.rotation;
    }

    public void GoBack()
    {
        transform.position = Vector3.Slerp(transform.position, currentSlot.primaryPosition, 10f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, primaryRotation, 10f * Time.deltaTime);
        currentSlot.isEmpty = false;
    }

    public void AttachToHolder(CardSlot slot)
    {
        currentSlot = slot;
    }

    public void CheckHolder()
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("CardHolder");
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layer_mask))
        {
            CardSlot hitCardSlot = hit.collider.GetComponent<CardSlot>();
            if (hitCardSlot)
            {
                currentSlot = hitCardSlot;
                hitCardSlot.isEmpty = false;
            }
        }
    }
}
