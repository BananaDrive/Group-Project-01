using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private List<InteractableObject> items = new List<InteractableObject>();
    private InteractableObject currentItem;

    public void AddItem(InteractableObject item)
    {
        items.Add(item);
        item.gameObject.SetActive(false); // Hides item in scene
    }

    public void EquipItem(int index)
    {
        if (index >= 0 && index < items.Count)
        {
            // Hide the current item if equipped
            if (currentItem != null)
            {
                currentItem.gameObject.SetActive(false);
            }

            // Equip the new item
            currentItem = items[index];
            currentItem.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(0); // Equip first item
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(1); // Equip second item
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipItem(2); // Equip third item
        }
        // Add more checks for additional items if needed
    }
}