using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    public Vector3 worldPosition;
    Plane plane = new Plane(Vector3.up, -2f);
    bool isSelected;
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
                    controlledCard = hitCard;
                    controlledCard.GetComponent<PrimaryPosition>().isSelected = true;
                    controlledCard.GetComponent<PrimaryPosition>().currentSlot.isEmpty = true;
                }
            }
        }

        if(controlledCard)
        {
            controlledCard.transform.position = Vector3.Lerp(controlledCard.transform.position, worldPosition, 10f * Time.deltaTime);
            controlledCard.transform.LookAt(worldPosition + Vector3.up * yValue, -Vector3.forward);
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckforCardSlot();
            if(controlledCard) controlledCard.GetComponent<PrimaryPosition>().isSelected = false;
            controlledCard = null;
        }
    }

    void CheckforCardSlot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("CardHolder");
        if (controlledCard && Physics.Raycast(worldPosition, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layer_mask))
        {
            CardSlot hitCardSlot = hit.collider.GetComponent<CardSlot>();
            if (hitCardSlot)
            {
                if (hitCardSlot.isEmpty)
                {
                    controlledCard.GetComponent<PrimaryPosition>().primaryPosition = hitCardSlot.primaryPosition;
                    hitCardSlot.AttachCard();
                    controlledCard.GetComponent<PrimaryPosition>().AttachToHolder(hitCardSlot,hitCardSlot.isBattleSlot);
                }
                else if (controlledCard.GetComponent<PrimaryPosition>().oldSlot == hitCardSlot)
                {
                    controlledCard.GetComponent<PrimaryPosition>().primaryPosition = hitCardSlot.primaryPosition;
                    hitCardSlot.AttachCard();
                    controlledCard.GetComponent<PrimaryPosition>().AttachToHolder(hitCardSlot, hitCardSlot.isBattleSlot);
                }
            }
        }
    }
}
