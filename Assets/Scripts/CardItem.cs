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
     [SerializeField] TextMeshProUGUI cooldownCounter;
    [SerializeField] GameObject cooldownOverlay;
    [SerializeField] TextMeshProUGUI currentCooldownCounter;
    public bool isRewardCard;

    [SerializeField] Image cardImage;
    [SerializeField] Image diceCount;

    [SerializeField] List<RawImage> diceSideimages = new List<RawImage>(6);

    [Header("Data")]
    [SerializeField] public BaseItem item;
    [SerializeField] DiceSideDatabase diceSideData;

    [HideInInspector]
    public List<DiceGameObject> childDice;

    public CombatAgent owner;

    [HideInInspector]
    public int inactiveForTurns = 0;

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
        cooldownCounter.text = item.cooldown.ToString();

        for (int i = 0; i < 6; i++)
        {
            diceSideimages[i].material = diceSideData.GetMaterial(item.diceSides[i]);
        }
    }

    public void SetOnCooldown()
    {
        if(item.cooldown <= 0) return;

        inactiveForTurns = item.cooldown;

        cooldownOverlay.SetActive(true);
        currentCooldownCounter.text = inactiveForTurns.ToString();

        GetComponent<CardPosition>().isMovable = false;
    }

    public void CooldownDecrement()
    {
        if(inactiveForTurns <= 0) return;

        inactiveForTurns--;
        currentCooldownCounter.text = inactiveForTurns.ToString();

        if(inactiveForTurns <= 0)
        {
            cooldownOverlay.SetActive(false);
            GetComponent<CardPosition>().isMovable = true;
        }
    }
}
