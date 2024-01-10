using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [HideInInspector] public bool isPlaced = false;
    [SerializeField] Material originalMaterial = null;

    public void PlaceObject()
    {
        if (!isPlaced)
        {
            GameObject copy = Instantiate(gameObject, transform.position, transform.rotation);
            MeshRenderer copyRenderer = copy.GetComponentInChildren<MeshRenderer>();
            copyRenderer.material = originalMaterial;
            copyRenderer.enabled = true;
            isPlaced = true;
        }
    }
}
