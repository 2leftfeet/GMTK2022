using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderline : MonoBehaviour
{
    private LineRenderer line;
    public GameObject targetPoint;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
    }

    [ContextMenu("Re-draw")]
    void UpdateLines(Vector3 target)
    {
        Reset();
        line.positionCount = 2;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target);
    }

    // Update is called once per frame
    void Update()
    {
        // in the start enough. not need to do it un the update to just draw the line
    }
    public void Reset()
    {
        line.positionCount = 0;
    }

}
