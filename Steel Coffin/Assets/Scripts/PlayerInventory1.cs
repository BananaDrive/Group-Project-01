using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory1 : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>(3); 
    public Transform hand; 
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode[] hotbarKeys;
    public Image[] hotbarImages;
    public TextMeshProUGUI pickupMessage;
    public int currentSlot = 0;
    public bool keyPickup = false;

    void Start()
    {
        hotbarKeys = new KeyCode[3] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3};

        inventory = new List<GameObject>(new GameObject[3]);

    }

    void Update()
    {
        HandlePickup();
        HandleHotbarSwitch();
        CheckForKeys();
    }

    void HandlePickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float sphereRadius = 2.5f;
            float maxDistance = 10f;

            Vector3 origin = transform.position;

            Collider[] colliders = Physics.OverlapSphere(origin, sphereRadius);

            foreach (Collider collider in colliders)
            {
                GameObject item = collider.gameObject;

                if (Vector3.Distance(origin, item.transform.position) <= maxDistance)
                {
                    if (item.CompareTag("PickupItem"))
                    {
                        Debug.Log("Pickup item detected: " + item.name);

                        if (Input.GetKeyDown(pickupKey))
                        {
                            AddToInventory(item);
                            CheckForKeys();
                        }
                    }
                }
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
                
                
                return;
            }
        }
       

    }

    void HandleHotbarSwitch()
    {
        for (int i = 0; i < hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotbarKeys[i]))
            {
                currentSlot = i;
                UpdateHotbarUI();
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



    void CheckForKeys()
    {
        keyPickup = false;

        foreach (var item in inventory)
        {
            if (item != null && (item.name == "Key1" && item.name == "Key2"))
            {
                keyPickup = true;
                break;
            }
        }
    }
}
