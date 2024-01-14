using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectInteractorWithControllers : MonoBehaviour
{
    private Rigidbody grabbedRigidbody = null;
    private GameObject objectInGrabRange = null;
    private GrabbableObjectWithControllers grabbable = null;

    public void Grab()
    {
        if (grabbedRigidbody) return;
        
        if(objectInGrabRange.TryGetComponent(out grabbable))
        {
            objectInGrabRange.transform.SetParent(transform);
            grabbedRigidbody = objectInGrabRange.GetComponent<Rigidbody>();
            grabbedRigidbody.isKinematic = true;
            grabbedRigidbody.velocity = Vector3.zero;
            grabbable.SetGrabbedState(true);
        }  
    }

    public void Release()
    {
        if (grabbedRigidbody == null) return;
        grabbedRigidbody.transform.SetParent(null);
        grabbedRigidbody.isKinematic = false;
        grabbedRigidbody = null;
        grabbable.SetGrabbedState(false);
        grabbable = null;
    }

    private void OnTriggerStay(Collider other)
    {
        objectInGrabRange = other.gameObject;
    }
}
