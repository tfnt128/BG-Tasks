using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    [SerializeField] private Image borderImage;

    //begin pre-written code
    public event Action<UIInventoryItem> OnItemClicked, OnRightMouseBtnClick;

    public bool empty = true;
    //end pre-written code

    public void Awake()
    {
        ResetData();
        Deselect();
    }
    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);
        empty = true;
    }
    public void Deselect()
    {
        borderImage.enabled = false;
    }
    public void SetData(Sprite sprite)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = sprite;
        empty = false;
    }

    public void Select()
    {
        borderImage.enabled = true;
    }
    //begin pre-written code

    public void OnPointerClick(BaseEventData data)
    {

        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Left)
        {
            OnItemClicked?.Invoke(this);
        }
        else
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
    }
    //end pre-written code

}
