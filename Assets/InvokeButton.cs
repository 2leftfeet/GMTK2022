using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InvokeButton : MonoBehaviour
{
    Button button;

    public void Start()
    {
        button = GetComponent<Button>();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            button.onClick.Invoke();
        }
    }
}
