using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderline : MonoBehaviour
{
    private LineRenderer line;
    public GameObject targetPoint;

    [SerializeField] float middlePointY = 1f;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
    }

    void UpdateLines(Vector3 target)
    {
        Reset();
        line.positionCount = 3;
        line.SetPosition(0, transform.position);
        var middlePoint = transform.position + (target - transform.position) / 2;
        middlePoint += Vector3.up * middlePointY;
        line.SetPosition(1, middlePoint);
        line.SetPosition(2, target);
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            UpdateLines(targetPoint.transform.position);
        }
    }
    public void Reset()
    {
        line.positionCount = 0;
    }

}
