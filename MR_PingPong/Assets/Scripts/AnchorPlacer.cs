using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnchorPlacer : MonoBehaviour
{
    [SerializeField] UnityEvent<GameObject> OnAnchorPlaced;

    public void PlaceMRUKAnchor(MRUKAnchor anchor)
    {
        PlaceableObject placeable = anchor.GetComponentInChildren<PlaceableObject>();
        if (placeable)
        {
            GameObject placedObject = placeable.PlaceObject();
            OnAnchorPlaced.Invoke(placedObject);
        }
    }
}
