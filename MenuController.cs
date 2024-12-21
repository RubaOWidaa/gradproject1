using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenuPanel; // Main menu with buttons
    [SerializeField] private GameObject assemblyGuideUI; // UI >> Assembly Guide mode
    [SerializeField] private GameObject modelViewUI; // UI >> to 3D Model mode

    [Header("Model Parent")]
    [SerializeField] private GameObject modelParent; // Parent containing the model

    private AssemblyGuide assemblyGuideScript;
    private SelectableComponent selectableComponentsScript;

    private void Start()
    {
        // Get scripts from the model parent
        assemblyGuideScript = modelParent.GetComponent<AssemblyGuide>();
        selectableComponentsScript = modelParent.GetComponent<SelectableComponent>();

        // Disable scripts initially
        if (assemblyGuideScript != null) assemblyGuideScript.enabled = false;
        if (selectableComponentsScript != null) selectableComponentsScript.enabled = false;

        // Ensure only the MainMenuPanel is active initially
        mainMenuPanel.SetActive(true);
        assemblyGuideUI.SetActive(false);
        modelViewUI.SetActive(false);
    }

    public void On3DModelViewSelected()
    {
        // Hide Main Menu and activate 3D Model View
        mainMenuPanel.SetActive(false);
        modelViewUI.SetActive(true);

        if (selectableComponentsScript != null)
        {
            selectableComponentsScript.enabled = true; // Enable SelectableComponents script
        }

        if (assemblyGuideScript != null)
        {
            assemblyGuideScript.enabled = false; 
        }
    }

    public void OnAssemblyGuideSelected()
    {
        // Hide Main Menu and activate Assembly Guide
        mainMenuPanel.SetActive(false);
        assemblyGuideUI.SetActive(true);

        if (assemblyGuideScript != null)
        {
            assemblyGuideScript.enabled = true; 
        }

        if (selectableComponentsScript != null)
        {
            selectableComponentsScript.enabled = false; 
        }
    }
}

