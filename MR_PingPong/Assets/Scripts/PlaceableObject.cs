using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [HideInInspector] public bool isPlaced = false;
    [SerializeField] Material originalMaterial = null;
    MeshRenderer[] renderers;

    public void PlaceObject()
    {
        if (!isPlaced)
        {
            // Place a copy of this object at the current position and rotation
            GameObject copy = Instantiate(gameObject, transform.position, transform.rotation); 
            SetMaterials(copy);
            isPlaced = true;

            // Destroy this object
            Destroy(gameObject);
        }
    }

    void SetMaterials(GameObject newObject)
    {
        MeshRenderer[] renderers = newObject.GetComponentsInChildren<MeshRenderer>();

        if (renderers.Length > 0)
        {
            foreach(MeshRenderer renderer in renderers)
            {
                renderer.sharedMaterial = originalMaterial;
                renderer.enabled = true;
            }
        }
    }
}
