using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuTable : MonoBehaviour
{
    public Button restartButton;
    public Button retryButton;

    public bool isCalled;

    Vector3 startPosition;
    Vector3 calledPosition;

    [SerializeField] float ammountToMove = 10f;


    private void Start()
    {
        startPosition = transform.position;
        calledPosition += startPosition - Vector3.back * ammountToMove;
    }
    void Update()
    {
        if (!isCalled) transform.position = Vector3.Slerp(transform.position, startPosition, 10f * Time.deltaTime);
        else if (isCalled) transform.position = Vector3.Slerp(transform.position, calledPosition, 10f * Time.deltaTime);
    }
}
