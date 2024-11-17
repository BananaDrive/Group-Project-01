using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>(9); 
    public Transform hand; 
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode[] hotbarKeys;
    public Image[] hotbarImages;
    public Text pickupMessage;
    public int currentSlot = 0; 

    void Start()
    {
        hotbarKeys = new KeyCode[9] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

        for (int i = 0; i < inventory.Capacity; i++)
        {
            inventory.Add(null);
        }

        pickupMessage.text = "";
    }

    void Update()
    {
        HandlePickup();
        HandleHotbarSwitch();
    }

    void HandlePickup()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            RaycastHit hit;
            float sphereRadius = .5f;
            float maxDistance = 10f; 

            Vector3 origin = transform.position;
            Vector3 direction = transform.forward;

            if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance))
            {
                GameObject item = hit.collider.gameObject;
                if (item.CompareTag("PickupItem"))
                {
                    pickupMessage.text = "(E) " + item.name;
                    if (Input.GetKeyDown(pickupKey))
                    {
                        AddToInventory(item);
                    }
                    
                }
                else
                {
                    pickupMessage.text = "";
                }
            }
            else
            {
                pickupMessage.text = "";
            }
        }
    }



    void AddToInventory(GameObject item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                item.SetActive(false);
                UpdateHotbarUI();
                Debug.Log("Item added to inventory");
                return;
            }
        }
       
        Debug.Log("Inventory is full! Cannot pick up the item.");
    }

    void HandleHotbarSwitch()
    {
        for (int i = 0; i < hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotbarKeys[i]))
            {
                currentSlot = i;
                UpdateHotbarUI();
                EquipItem();
            }
        }
    }

    void UpdateHotbarUI()
    {
        for (int i = 0; i < hotbarImages.Length; i++)
        {
            hotbarImages[i].color = (i == currentSlot) ? Color.green : Color.white;
        }
    }

    void EquipItem()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] != null)
            {
                inventory[i].SetActive(i == currentSlot);
                if (i == currentSlot)
                {
                    inventory[i].transform.position = hand.position;
                    inventory[i].transform.SetParent(hand);
                }
            }
        }
    }
}
