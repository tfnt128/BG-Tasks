using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private UIInventoryDescription inventoryDescription;
    [SerializeField] private ItemActionPanel actionPanel;

    private List<UIInventoryItem> listOfUiItems = new List<UIInventoryItem>();
    public event Action<int> OnDescriptionRequested;
    public event Action<int> OnItemActionRequested;

    private void Awake()
    {
        Hide();
        inventoryDescription.ResetDescription();
    }
     //begin pre-written code
    
    public void InitializeInventoryUI(int inventorySize)
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        listOfUiItems.Clear();

        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = new Vector3(1f, 1f, 1f);
            listOfUiItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    //end pre-written code

    internal void ResetAllItems()
    {
        foreach (var item in listOfUiItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        inventoryDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        listOfUiItems[itemIndex].Select();
    }

    internal void UpdateSelection(int itemIndex)
    {
        DeselectAllItems();
        listOfUiItems[itemIndex].Select();
    }

    public void UpdateData(int itemIndex, Sprite itemImage)
    {
        if (listOfUiItems.Count > itemIndex)
        {
            listOfUiItems[itemIndex].SetData(itemImage);
        }
    }

    public void ResetData(int itemIndex)
    {
        if (listOfUiItems.Count > itemIndex)
        {
            listOfUiItems[itemIndex].ResetData();
        }
    }

    private void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUiItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButon(actionName, performAction);
    }

    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfUiItems[itemIndex].transform.position;
    }

    private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
    {
        int index = listOfUiItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        OnItemActionRequested?.Invoke(index);
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfUiItems)
        {
            item.Deselect();
        }
    }

    public void ResetSelection()
    {
        inventoryDescription.ResetDescription();
        DeselectAllItems();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
    }
    public void HideOnlyActionPanel()
    {
        actionPanel.Toggle(false);
    }
}
