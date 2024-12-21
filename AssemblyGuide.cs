using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssemblyGuide : MonoBehaviour
{
    public GameObject layerToggleUIPrefab; // Prefab for step indication
    [SerializeField] private Transform uiParent; // Panel to hold the UI steps
    [SerializeField] private Button nextButton; // Button to navigate to the next step
    [SerializeField] private Button previousButton; // Button to navigate to the previous step
    [SerializeField] private TMP_Text stepIndicator; // text :current step
   
    // Store component objects
    private Transform[] components; 
    private int currentStep = 0; 
    private Transform[] nonComponentObjects; // Store board, silk, nets

    private void Start()
    {
        InitializeComponents();
        UpdateUI();
    }

    private void InitializeComponents()
    {
        var componentList = new System.Collections.Generic.List<Transform>();
        var excludedList = new System.Collections.Generic.List<Transform>();

        foreach (Transform child in transform)
        {
            string lowerName = child.name.ToLower();
            if (lowerName.Contains("silk") || lowerName.Contains("board") || lowerName.Contains("no_net") || lowerName.Contains("nets"))
            {
                excludedList.Add(child); 
                child.gameObject.SetActive(false); // Deactivate 
            }
            else
            {
                
                componentList.Add(child);
                child.gameObject.SetActive(false); // Deactivate initially
                CreateStepIndicator(componentList.Count, child.name); // Create a step indicator
            }
        }

        components = componentList.ToArray();
        nonComponentObjects = excludedList.ToArray();

        // Activate the first component
        if (components.Length > 0)
        {
            components[currentStep].gameObject.SetActive(true);
        }

        // Set up button listeners
        if (nextButton != null)
            nextButton.onClick.AddListener(GoToNextStep);

        if (previousButton != null)
            previousButton.onClick.AddListener(GoToPreviousStep);
    }

    private void CreateStepIndicator(int stepNumber, string componentName)
    {
        if (layerToggleUIPrefab == null || uiParent == null)
        {
            Debug.LogWarning("LayerToggleUIPrefab is not assigned.");
            return;
        }

        GameObject toggleGO = Instantiate(layerToggleUIPrefab, uiParent);
        TMP_Text label = toggleGO.GetComponentInChildren<TMP_Text>();

        if (label != null)
        {
            label.text = $"{stepNumber}: {componentName}";
        }

        Toggle toggle = toggleGO.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.interactable = false; 
        }
    }

    private void GoToNextStep()
    {
        if (currentStep < components.Length - 1)
        {
            components[currentStep].gameObject.SetActive(false);
            currentStep++;
            components[currentStep].gameObject.SetActive(true);
            UpdateUI();
        }
    }

    private void GoToPreviousStep()
    {
        if (currentStep > 0)
        {
            components[currentStep].gameObject.SetActive(false);
            currentStep--;
            components[currentStep].gameObject.SetActive(true);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (stepIndicator != null)
        {
            stepIndicator.text = $"Step {currentStep + 1} of {components.Length}: {components[currentStep].name}";
        }

        if (nextButton != null)
        {
            nextButton.interactable = currentStep < components.Length - 1;
        }

        if (previousButton != null)
        {
            previousButton.interactable = currentStep > 0;
        }
    }
}
