using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool isInteracted = false;
    public bool isWeapon;
    public bool isObject;

    public Firearm firearm;
    public int weaponIndexToUnlock;

    public ModelManager modelManager;
    public int modelIndexToActivate;

    public void Interact()
    {
        if (!isInteracted)
        {
            // Add the item to the player's inventory
            Inventory playerInventory = FindObjectOfType<Inventory>(); // Find the player's inventory
            if (playerInventory != null)
            {
                playerInventory.AddItem(this); // Add this item to the inventory
            }
            else
            {
                Debug.LogWarning("Player inventory not found!");
            }

            // Ensure only this item is active
            ItemManager.Instance.SetActiveItem(this);

            // Activate weapon or object-specific functionality
            if (isWeapon)
            {
                UnlockWeapon();
            }

            if (isObject)
            {
                ActivateModel();
            }

            isInteracted = true; // Mark as interacted
        }
        else
        {
            Debug.Log("Already interacted with: " + gameObject.name);
        }
    }

    private void UnlockWeapon()
    {
        if (firearm != null)
        {
            firearm.UnlockWeapon(weaponIndexToUnlock);
            firearm.SetupWeapon(weaponIndexToUnlock);
            Debug.Log($"Unlocked weapon at index: {weaponIndexToUnlock}");
        }
        else
        {
            Debug.LogError("Firearm reference is not set!");
        }
    }

    private void ActivateModel()
    {
        if (modelManager != null)
        {
            modelManager.ActivateModel(modelIndexToActivate);
            Debug.Log($"Activated model at index: {modelIndexToActivate}");
        }
        else
        {
            Debug.LogError("ModelManager reference is not set!");
        }
    }

    public void CheckAndDisable()
    {
        if (isWeapon)
        {
            if (firearm != null)
            {
                firearm.DisableAllWeapons();
                Debug.Log("Disabled all weapon objects.");
            }
            else
            {
                Debug.LogError("Firearm reference is not set!");
            }
        }
        else if (isObject)
        {
            if (modelManager != null)
            {
                modelManager.DisableAllModels();
                Debug.Log("Disabled all models.");
            }
            else
            {
                Debug.LogError("ModelManager reference is not set!");
            }
        }
    }
}