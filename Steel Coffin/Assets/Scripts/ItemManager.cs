using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    private InteractableObject currentActiveItem;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetActiveItem(InteractableObject newItem)
    {
        if (currentActiveItem != null && currentActiveItem != newItem)
        {
            currentActiveItem.CheckAndDisable();
        }

        currentActiveItem = newItem;
    }

    public void Interact()
    {
        if (!isInteracted)
        {
            ItemManager.Instance.SetActiveItem(this);

            if (isWeapon)
            {
                UnlockWeapon();
            }

            if (isObject)
            {
                ActivateModel();
            }

            isInteracted = true;
        }

        else
        {
            Debug.Log("Already interacted with: " + gameObject.name);
        }
    }

}
