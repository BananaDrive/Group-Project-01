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
            currentActiveItem.CheckAndDisable(); // Disable the previously active item
        }

        currentActiveItem = newItem;
    }
}
