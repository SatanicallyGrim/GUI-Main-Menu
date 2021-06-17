using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] public List<Items> inventory = new List<Items>();
    [SerializeField] private bool showIMGUIInventory = true;
    [HideInInspector] public Items selectedItem = null;

    #region Canvas
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject inventoryGameobject;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private GameObject filterContent;

    [Header("Item Display")]
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDescription;
    #endregion

    #region Display Inventory
    private Vector2 scrollPosition;
    private string sortType = "All";
    #endregion
    private void Start()
    {
        DisplayFiltersCanvas();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryGameobject.activeSelf)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryGameobject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                inventoryGameobject.SetActive(true);
                DisplayItemCanvas();
            }

        }
    }

    private void DisplayFiltersCanvas()
    {
        List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
        itemTypes.Insert(0, "All");

        for (int i = 0; i < itemTypes.Count; i++)
        {
            Button buttonGo = Instantiate<Button>(buttonPrefab, filterContent.transform);
            Text buttonText = buttonGo.GetComponentInChildren<Text>();
            buttonGo.name = itemTypes[i] + " Filters";
            buttonText.text = itemTypes[i];
            int x = i;
            //buttonGo.onClick.AddListener(() => { sortType = itemTypes[i]; });
            buttonGo.onClick.AddListener(delegate { ChangeFilter(itemTypes[x]); });
        }
    }
    private void ChangeFilter(string itemType)
    {
        sortType = itemType;
        DisplayItemCanvas();
    }
    void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    private void DisplayItemCanvas()
    {
        DestroyAllChildren(inventoryContent.transform);
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGo = Instantiate<Button>(buttonPrefab, inventoryContent.transform);
                Text buttonText = buttonGo.GetComponentInChildren<Text>();
                buttonGo.name = inventory[i].Name + " Button";
                buttonText.text = inventory[i].Name;
                int x = i;
                buttonGo.onClick.AddListener(delegate { DisplaySelectedItemOnCanvas(inventory[x]); });
            }
        }
    }
    public void RemoveItemFromInventory(Items _item)
    {
        inventory.Remove(_item);
        DisplayItemCanvas();
    }
    public void AddItemToInventory(Items _item, int _count)
    {
        Items foundItem = inventory.Find((x) => x.Name == _item.Name);
        if (foundItem == null)
        {
            inventory.Add(_item);
        }
        else
        {
            foundItem.Amount += _count;
        }
    }
    public void AddItemToInventory(Items _item)
    {
        AddItemToInventory(_item, _item.Amount);
    }
    private void DisplaySelectedItemOnCanvas(Items _item)
    {
        selectedItem = _item;
        itemImage.texture = selectedItem.Icon;
        itemName.text = selectedItem.Name;
        itemDescription.text = selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount;
    }

    private void OnGUI()
    {
        if (showIMGUIInventory)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
            itemTypes.Insert(0, "All");

            for (int i = 0; i < itemTypes.Count; i++)
            {
                if (GUI.Button(new Rect((Screen.width / itemTypes.Count) * i, 10, Screen.width / itemTypes.Count, 20), itemTypes[i]))
                {
                    sortType = itemTypes[i];
                }
            }
            Display();

            if (selectedItem != null)
            {
                DisplaySelectedItem();
            }
        }
    }
    private void DisplaySelectedItem()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 3, Screen.width / 5, Screen.height / 5), selectedItem.Icon);
        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5), Screen.width / 7, Screen.height / 15), selectedItem.Name);
        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3) + (Screen.height / 3), Screen.width / 5, Screen.height / 5), selectedItem.Description + "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
    }

    private void Display()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40), scrollPosition, new Rect(0, 0, 0, inventory.Count * 30), false, true);
        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                if (GUI.Button(new Rect(30, 0 + (count * 30), 200, 30), inventory[i].Name))
                {
                    selectedItem = inventory[i];
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }
}
