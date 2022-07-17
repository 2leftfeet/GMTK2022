using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightText : MonoBehaviour
{
    [SerializeField] GameObject text;
    private void OnMouseExit()
    {
        text.SetActive(false);
    }

    private void OnMouseEnter()
    {
        text.SetActive(true);
    }
}
