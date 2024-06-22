using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text titleText;
    [SerializeField] TMP_Text descriptionText;

    private void Awake() 
    {
        ResetDescription();
    }

    //begin pre-written code
    public void ResetDescription()
    {
        itemImage.gameObject.SetActive(false);
        titleText.text = "";  
        descriptionText.text = "";  
    }
    //end pre-written code

    public void SetDescription(Sprite sprite, String itemName, string itemDescription)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        titleText.text = itemName;  
        descriptionText.text = itemDescription;  
    }
}
