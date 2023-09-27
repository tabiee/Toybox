using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Item class to represent an item in the inventory
[System.Serializable]
public class Item
{
    public string name;
    public int quantity;
}

// Inventory class to manage the player's inventory
public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>(); // List to store the items

    public void AddItem(string itemName)
    {
        Item item = GetItem(itemName);
        if (item != null)
        {
            // Item already exists, increase the quantity
            item.quantity++;
        }
        else
        {
            // Item does not exist, add it to the inventory with quantity 1
            Item newItem = new Item();
            newItem.name = itemName;
            newItem.quantity = 1;
            items.Add(newItem);
        }

        // Update UI here to reflect the changes in the inventory
    }

    public void RemoveItem(string itemName)
    {
        Item item = GetItem(itemName);
        if (item != null)
        {
            // Decrease the quantity
            item.quantity--;

            if (item.quantity <= 0)
            {
                // If quantity reaches zero, remove the item from the inventory
                items.Remove(item);
            }
        }

        // Update UI here to reflect the changes in the inventory
    }

    public int GetItemQuantity(string itemName)
    {
        Item item = GetItem(itemName);
        return (item != null) ? item.quantity : 0;
    }

    private Item GetItem(string itemName)
    {
        return items.Find(item => item.name == itemName);
    }
}

// Inventory UI script to update the UI based on the inventory data
public class InventoryUI : MonoBehaviour
{
    public Text item1Text; // Reference to the UI Text component for item 1
    public Text item2Text; // Reference to the UI Text component for item 2
    public Text item3Text; // Reference to the UI Text component for item 3

    public Inventory inventory; // Reference to the Inventory script

    private void UpdateUI()
    {
        // Update the UI Text components with the inventory data
        item1Text.text = "Item 1: " + inventory.GetItemQuantity("Item1");
        item2Text.text = "Item 2: " + inventory.GetItemQuantity("Item2");
        item3Text.text = "Item 3: " + inventory.GetItemQuantity("Item3");
    }

    private void Start()
    {
        // Register for inventory change events
        //inventory.OnInventoryChanged += UpdateUI;

        // Update the UI initially
        UpdateUI();
    }
}

