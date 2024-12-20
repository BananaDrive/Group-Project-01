using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>(3);
    public Transform hand;
    public KeyCode pickupKey = KeyCode.E;
    public KeyCode[] hotbarKeys;
    public Image[] hotbarImages;
    public TextMeshProUGUI pickupMessage;
    public int currentSlot = 0;
    public bool keyPickup = false;
    public AudioSource Keys;
    public GameManager Gm;

    void Start()
    {
        Gm = gameObject.GetComponent<GameManager>();
        hotbarKeys = new KeyCode[3] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

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

                        AddToInventory(item);
                        CheckForKeys();
                        Keys.Play();
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
                ShowItemImage(item.name); 
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

    void ShowItemImage(string itemName)
    {
        switch (itemName)
        {
            case "Key1":
                hotbarImages[0].gameObject.SetActive(true);
                break;
            case "Key2":
                hotbarImages[1].gameObject.SetActive(true); 
                break;
            case "Key3":
                hotbarImages[2].gameObject.SetActive(true);
                break;
        }
    }

    void CheckForKeys()
    {
        keyPickup = false;

        foreach (var item in inventory)
        {
            if (item != null && (item.name == "Key1" || item.name == "Key2" || item.name == "Key3"))
            {
                keyPickup = true;
                break;
            }
        }
    }
}
