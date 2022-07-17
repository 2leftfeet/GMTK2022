using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InvokeButton : MonoBehaviour
{
    Button button;

    bool isUsable;

    [SerializeField] GameObject highlightParticle;

    public void Start()
    {
        button = GetComponent<Button>();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isUsable)
        {
            button.onClick.Invoke();
        }
    }

    public void EnableHighlight()
    {
        highlightParticle.SetActive(true);
        isUsable = true;
    }

    public void DisableHighlight()
    {
        highlightParticle.SetActive(false);
        isUsable = false;
    }
}
