using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryPosition : MonoBehaviour
{
    public Transform cachedPosition;

    private void OnEnable()
    {
        cachedPosition = transform;
    }
}
