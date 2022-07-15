using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public Transform cachedPosition;

    private void OnEnable()
    {
        cachedPosition = transform;
    }
}
