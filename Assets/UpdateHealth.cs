using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthAmmount;
    [SerializeField] Renderer rend;
    private void Update()
    {
        rend.material.SetFloat("_Health", float.Parse(healthAmmount.text));
    }
}
