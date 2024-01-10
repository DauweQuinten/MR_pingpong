using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPlacer : MonoBehaviour
{
    public void PlaceMRUKAnchor(MRUKAnchor anchor)
    {
        PlaceableObject placeable = anchor.GetComponentInChildren<PlaceableObject>();
        if (placeable)
        {
            placeable.PlaceObject();
        }
    }
}
