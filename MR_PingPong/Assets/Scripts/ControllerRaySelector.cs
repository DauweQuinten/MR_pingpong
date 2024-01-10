using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ControllerRaySelector : MonoBehaviour
{
    #region inspector variables
    [SerializeField, Tooltip("Only select MRUK anchors with these labels")] MRUKAnchor.SceneLabels validLabels;
    [SerializeField, Tooltip("This material gets applied to the MRUK anchor on hover. Let empty if you don't want to highlight the anchor")] Material highlightMaterial = null;
    [SerializeField, Tooltip("Toggle between left and right controller")] bool isLeftHanded = false;
    [SerializeField, Tooltip("if true, the last selected anchor stays highlighted until another anchor gets selected")] bool keepLastValidSelected = false;
    [SerializeField] UnityEvent<MRUKAnchor> OnAnchorClicked;
    #endregion

    #region private variables
    OVRInput.Controller controller = OVRInput.Controller.RTouch;
    MRUKAnchor currentSelectedAnchor = null;
    MRUKAnchor prevSelectedAnchor = null;
    MRUKAnchor lastSelectedAnchor = null;
    MRUKAnchor prevLastSelectedAnchor = null;
    #endregion

    private void Start()
    {
        SetController(isLeftHanded);
    }

    void Update()
    {
        CheckMRUKRayCastFromController(controller, out RaycastHit hit, out MRUKAnchor anchorHit);
        HandleAnchorSelection(anchorHit);
        HandleAnchorHighlighting();
        HandleInputEvents();
    }

    #region Handler functions

    void HandleAnchorSelection(MRUKAnchor anchorHit)
    {
        if (anchorHit != null)
        {
            bool anchorIsValid = validLabels.HasFlag(anchorHit.GetLabelsAsEnum());
            if (anchorIsValid)
            {
                currentSelectedAnchor = anchorHit;
                lastSelectedAnchor = anchorHit;
            }
            else
            {
                currentSelectedAnchor = null;
            }
        }
        else
        {
            currentSelectedAnchor = null;
        }
    }

    void HandleAnchorHighlighting()
    {
        if (highlightMaterial != null)
        {
            if (keepLastValidSelected)
            {
                if (lastSelectedAnchor != prevLastSelectedAnchor)
                {
                    HighlightMRUKAnchor(lastSelectedAnchor, true);
                    HighlightMRUKAnchor(prevLastSelectedAnchor, false);
                    prevLastSelectedAnchor = lastSelectedAnchor;
                }
            }
            else
            {
                if (currentSelectedAnchor != prevSelectedAnchor)
                {
                    HighlightMRUKAnchor(prevSelectedAnchor, false);
                    HighlightMRUKAnchor(currentSelectedAnchor, true);
                    prevSelectedAnchor = currentSelectedAnchor;
                }
            }
        }
    }

    void HandleInputEvents()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            MRUKAnchor anchor = GetSelectedAnchor();
            if(anchor != null)
            {
                OnAnchorClicked.Invoke(anchor);
            }
        }
    }

    private void OnDisable()
    {
        HighlightMRUKAnchor(GetSelectedAnchor(), false);
    }

    #endregion

    #region helper functions
    void CheckMRUKRayCastFromController(OVRInput.Controller controller, out RaycastHit hit, out MRUKAnchor anchorHit)
    {
        // Create a ray from the controller to the forward direction
        Vector3 rayOrigin = OVRInput.GetLocalControllerPosition(controller);
        Vector3 rayDirection = OVRInput.GetLocalControllerRotation(controller) * Vector3.forward;
        Ray ray = new Ray(rayOrigin, rayDirection);
        hit = new RaycastHit();
        anchorHit = null;
        MRUK.Instance?.GetCurrentRoom()?.Raycast(ray, Mathf.Infinity, out hit, out anchorHit);
    }

    void SetController(bool isLeftHanded)
    {
        if (isLeftHanded)
        {
            controller = OVRInput.Controller.LTouch;
        }
        else
        {
            controller = OVRInput.Controller.RTouch;
        }
    }

    void HighlightMRUKAnchor(MRUKAnchor anchor, bool isHighlighted)
    {
        MeshRenderer[] renderers = anchor?.gameObject.GetComponentsInChildren<MeshRenderer>();
        if (!anchor || renderers.Length < 1) return;

        if (isHighlighted)
        {
            foreach(MeshRenderer renderer in renderers)
            {
                renderer.material = highlightMaterial;
                renderer.enabled = true;
            }
        }
        else
        {
            foreach (MeshRenderer renderer in renderers)
            {
                renderer.material = highlightMaterial;
                renderer.enabled = false;
            }
        }
    }

    public MRUKAnchor GetCurrentHoveredAnchor()
    {
        return currentSelectedAnchor;
    }

    public MRUKAnchor GetLastHoveredAnchor()
    {
        return lastSelectedAnchor;
    }

    public MRUKAnchor GetSelectedAnchor()
    {
        return keepLastValidSelected ? lastSelectedAnchor : currentSelectedAnchor;     
    }

    #endregion
}
