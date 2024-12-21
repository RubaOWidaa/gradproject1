using System.Collections;
using System.Collections.Generic;
using UnityEngine; //using UnityEngine.UI;

public class SelectableComponent : MonoBehaviour
{
    private Renderer objectRenderer; 
    private Color originalColor;
    private bool isSelected = false; 
    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); // Updated to use 'objectRenderer'
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void OnMouseEnter()
    {
        if (objectRenderer != null && !isSelected)
        {
            objectRenderer.material.color = Color.yellow; // Highlight on hover
        }
    }

    void OnMouseExit()
    {
        if (objectRenderer != null && !isSelected)
        {
            objectRenderer.material.color = originalColor; // Revert to original color if not selected
        }
    }

    void OnMouseDown()
    {
        if (isSelected)
        {
            // Double-click 
            isSelected = false;
            if (objectRenderer != null)
            {
                objectRenderer.material.color = originalColor; // Revert to original color
            }
            Debug.Log($"Deselected component: {gameObject.name}");
        }
        else
        {
            // Single click to select
            isSelected = true;
            if (objectRenderer != null)
            {
                objectRenderer.material.color = Color.green; // Change color to green for selected state
            }
            Debug.Log($"Selected component: {gameObject.name}");
            //infoText.text = $"Selected: {gameObject.name}";
        }
    }
}