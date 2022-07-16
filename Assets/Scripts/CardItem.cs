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

    [SerializeField] List<RawImage> diceSideimages = new List<RawImage>(6);

    [Header("Data")]
    [SerializeField] BaseItem item;
    [SerializeField] DiceSideDatabase diceSideData;

    [SerializeField] CardSlot currentSlot;

    public void Start()
    {
        if (!item) Debug.LogWarning("No Base item was assigned");
        else UpdateCardData();
    }

    public void UpdateCardData()
    {
        cardTitle.text = item.itemName;
        cardDescription.text = item.textBox;
        cardImage.sprite = item.image;
        diceCounter.text = "X" + item.diceCount.ToString();

        for (int i = 0; i < 6; i++)
        {
            diceSideimages[i].texture = diceSideData.GetTexture(item.diceSides[i]);
        }
    }
}