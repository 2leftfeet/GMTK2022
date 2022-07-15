using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardItem : MonoBehaviour
{

    [Header("Variables")]
    [SerializeField] TextMeshProUGUI cardTitle;
    [SerializeField] TextMeshProUGUI cardDescription;
    [SerializeField] TextMeshProUGUI diceCounter;

    [SerializeField] Image cardImage;
    [SerializeField] Image diceCount;

    [SerializeField] List<Image> diceSideimages = new List<Image>(6);

    public void Start()
    {

    }
}
