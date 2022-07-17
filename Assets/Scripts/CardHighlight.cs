using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHighlight : MonoBehaviour
{
    [SerializeField] float ammountToScale = 1f;
    [SerializeField] float ammountToMoveYAxis = 1f;
    [SerializeField] float duration = 5f;

    Transform child;

    Vector3 newScale;
    Vector3 newPosition;

    Vector3 oldScale;
    Vector3 oldPostion;

    bool isSelected;
    public bool isEnemy;

    CardPosition cardPosition;
    CardItem cardItem;

    [SerializeField] GameObject highlightParticle;

    private void Start()
    {
        oldScale = transform.localScale;
        child = gameObject.transform.GetChild(0).transform;
        cardPosition = GetComponent<CardPosition>();
        cardItem = GetComponent<CardItem>();
        oldPostion = Vector3.zero;
        if (isEnemy) cardPosition.isMovable = false;
    }

    private void OnMouseEnter()
    {
        if (cardItem.childDice.Count > 0)
        {
            highlightParticle.SetActive(true);
            foreach (DiceGameObject diceGameObject in cardItem.childDice)
            {
                diceGameObject.highlightEffect.SetActive(true);
            }
        }
        newScale = new Vector3(transform.localScale.x + ammountToScale, transform.localScale.y + ammountToScale, transform.localScale.z + ammountToScale);
        if (isEnemy) newPosition = new Vector3(child.localPosition.x + ammountToMoveYAxis, child.localPosition.y, child.localPosition.z + 0.05f);
        else if (!isEnemy) newPosition = new Vector3(child.localPosition.x - ammountToMoveYAxis, child.localPosition.y, child.localPosition.z + 0.05f);
        isSelected = true;
    }

    private void OnMouseExit()
    {
        isSelected = false;
        
        highlightParticle.SetActive(false);
        foreach (DiceGameObject diceGameObject in cardItem.childDice)
        {
            diceGameObject.highlightEffect.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!isEnemy) isSelected = false;
        }
        if (!isSelected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, oldScale, duration * Time.deltaTime);
            child.localPosition = Vector3.Lerp(child.localPosition, oldPostion, duration * Time.deltaTime);
        }
        else if (isSelected)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, duration * Time.deltaTime);
            child.localPosition = Vector3.Lerp(child.localPosition, newPosition, duration * Time.deltaTime);
        }
    }
}
