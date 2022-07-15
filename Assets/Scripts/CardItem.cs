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

    [Header("Data")]
    [SerializeField] BaseItem item;

    public void Start()
    {
        if (!item) Debug.LogWarning("No Base item was assigned");
        UpdateCardData();
    }

    public void UpdateCardData()
    {
        cardTitle.text = item.itemName;
        cardDescription.text = item.textBox;
        cardImage.sprite = item.image;
        diceCounter.text = "X" + item.diceCount.ToString();
    }
}
