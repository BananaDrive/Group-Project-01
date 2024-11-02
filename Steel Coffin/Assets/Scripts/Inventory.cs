using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<InteractableObject> items = new List<InteractableObject>();
    private InteractableObject currentItem;

    public void AddItem(InteractableObject item)
    {
        items.Add(item);
        item.gameObject.SetActive(false); // Hides Item in scene
    }

    public void EquipItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {

            // Hides Current Item if equiped
            if (currentItem != null)
            {
                currentItem.gameObject.SetActive(false);
            }

            // Equip new item
            currentItem = items[index];
            currentItem.gameObject.SetActive(true);
        }
    }

    public void Interact()
    {
        if (!isInteracted)
        {
            Inventory playerInventory = FindObjectOfType<Inventory>(); // Reference to player inventory
            if (playerInventory != null)
            {
                playerInventory.AddItem(this); // Adds Item to Inventory
            }

            isInteracted = true;
        }

        else
        {
            Debug.Log("Already Interacted with: " + gameObject.name);
        }
        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(0); // First Item
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(1); // Second Item
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipItem(2);
        }
        // Add more checks for more items
    }
}

// Attach script to Player Object