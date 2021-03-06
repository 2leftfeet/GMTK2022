using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTable : MonoBehaviour
{
    public bool isCalled;

    public List<CardSlot> cardSlots = new List<CardSlot>();

    Vector3 startPosition;
    Vector3 calledPosition;

    [SerializeField] float ammountToMove = 10f;


    private void Start()
    {
        startPosition = transform.position;
        calledPosition += startPosition - Vector3.forward * ammountToMove;
    }
    void Update()
    {
        if (!isCalled) transform.position = Vector3.Slerp(transform.position, startPosition, 10f * Time.deltaTime);
        else if (isCalled) transform.position = Vector3.Slerp(transform.position, calledPosition, 10f * Time.deltaTime);
    }
}
