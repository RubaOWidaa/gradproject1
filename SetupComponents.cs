using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupComponents : MonoBehaviour
{
    public GameObject prefabWithScript; 
    void Start()
    {
        AddCollidersAndScriptsToChildren(transform);
    }

    private void AddCollidersAndScriptsToChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Add a Mesh Collider to the child if it doesn't have one
            MeshCollider meshCollider = child.gameObject.GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                meshCollider = child.gameObject.AddComponent<MeshCollider>();
                Debug.Log($"MeshCollider added to {child.gameObject.name}");
            }
            if (prefabWithScript != null && child.gameObject.GetComponent(prefabWithScript.GetComponent<MonoBehaviour>().GetType()) == null)
            {  
                child.gameObject.AddComponent(prefabWithScript.GetComponent<MonoBehaviour>().GetType());
                Debug.Log($"Script {prefabWithScript.GetComponent<MonoBehaviour>().GetType().Name} added to {child.gameObject.name}");
            }

            AddCollidersAndScriptsToChildren(child);
        }
    }
}
