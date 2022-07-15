/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHighlight : MonoBehaviour
{
    Vector3 cachedScale;
    Vector3 cachedPosition;
    Transform cardFocusPostion;
    [SerializeField] float scaleAmount = 0.1f;
    [SerializeField] float duration = 0.1f;

    bool isFocused;
    bool isSelected;
    bool isRunning;

    GameObject focusedCard;
    public GameObject selectedCard;

    void Start()
    {
        cardFocusPostion = GameObject.Find("CardFocusPostion").transform;
        cachedScale = transform.localScale;
        cachedPosition = transform.position;
    }

    *//*    private void Update()
        {
            if (Input.GetKeyDown("space") & isSelected & !isFocused & !isRunning)
            {
                StartCoroutine(LerpPosition(cardFocusPostion.position, duration));
                ResetScale();
                isFocused = true;
            }
            else if (Input.GetKeyDown("space") & isFocused & !isRunning)
            {
                StartCoroutine(LerpPosition(cachedPosition, duration));
                isFocused = false;
            }
        }*//*

    public void OnMouseEnter()
    {
        if (!isFocused)
        {
            //AddScale();
            isSelected = true;
        }
    }

    public void OnMouseExit()
    {
        //ResetScale();
        isSelected = false;
    }

    public void ResetScale(Transform transform)
    {
        transform.localScale = cachedScale;
    }
    public void AddScale(Transform transform)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + scaleAmount, transform.localScale.z + scaleAmount);
    }

    IEnumerator LerpPosition(Transform cardPosition, Vector3 targetPosition, float duration)
    {
        isRunning = true;
        float time = 0;
        Vector3 startPosition = cardPosition.position;
        while (time < duration - 0.00001f)
        {
            cardPosition.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += (1 - (time / duration)) * Time.deltaTime;
            yield return null;
        }
        cardPosition.position = targetPosition;
        isRunning = false;
    }









    Ray ray;
    RaycastHit hit;
    Transform position;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Card")
            {
                selectedCard = hit.collider.gameObject;

                if (Input.GetKeyDown("space") & !isRunning & !isFocused)
                {
                    StartCoroutine(LerpPosition(selectedCard.transform, cardFocusPostion.position, duration));
                    position = selectedCard.GetComponent<PrimaryPosition>().cachedPosition;
                    focusedCard = selectedCard;
                    isFocused = true;
                }
                if (Input.GetKeyDown("space") & !isRunning & selectedCard != focusedCard)
                {
                    Debug.Log("TRUE");
                    StartCoroutine(LerpPosition(focusedCard.transform, position.position, duration));
                }
                Debug.Log(selectedCard);
            }
        }
    }
}

*/