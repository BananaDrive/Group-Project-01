using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public GameObject[] models; // Array to hold different model prefabs
    private int currentModelIndex = -1; // Default case with no model active

    public bool anyModelsActive;

    void Start()
    {
        // Deactivate all models at the start
        foreach (var model in models)
        {
            model.SetActive(false);
        }

        anyModelsActive = false;
    }

    void Update()
    {
        // Switch model based on input (you can customize this input handling)
        if (Input.GetKeyDown(KeyCode.Alpha1)) ActivateModel(0); // Activate Model 1
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ActivateModel(1); // Activate Model 2
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ActivateModel(2); // Activate Model 3
        // Add more models as needed

        // Test which model is active when 'F' is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            TestActiveModel();
        }
    }

    public void ActivateModel(int index)
    {
        // Deactivate all models first
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }

        // Activate the selected model
        if (index >= 0 && index < models.Length)
        {
            models[index].SetActive(true);
            currentModelIndex = index; // Update current model index
            anyModelsActive = true;
        }
        else
        {
            currentModelIndex = -1; // Reset to default
            anyModelsActive = false;
        }
    }

    private void TestActiveModel()
    {
        switch (currentModelIndex)
        {
            case 0:
                Debug.Log("Model 1 activated - Performing action for Model 1.");
                // poopy code
                break;
            case 1:
                Debug.Log("Model 2 activated - Performing action for Model 2.");
                // poopy code 2
                break;
            case 2:
                Debug.Log("Model 3 activated - Performing action for Model 3.");
                // poopy code 3
                break;
            default:
                Debug.Log("no have part");
                
                break;
        }
    }

    public void DisableAllModels()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }
        currentModelIndex = -1; // Reset current model index
        anyModelsActive = false; // Update the boolean
    }
}

