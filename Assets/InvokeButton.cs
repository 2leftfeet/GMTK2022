using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InvokeButton : MonoBehaviour
{
    Button button;

    [SerializeField] GameObject highlightParticle;

    public void Start()
    {
        button = GetComponent<Button>();
    }
    private void OnMouseOver()
    {
        highlightParticle.SetActive(true);
        if (Input.GetMouseButtonDown(0))
        {
            button.onClick.Invoke();
        }
    }

    private void OnMouseExit()
    {
        highlightParticle.SetActive(false);
    }
}
