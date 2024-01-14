using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DirectInteractorWithControllers : MonoBehaviour
{
    private Rigidbody grabbedRigidbody = null;
    private List<GameObject> objectsInRange = new List<GameObject>();
    private GrabbableObjectWithControllers grabbable = null;

    public void Grab()
    {
        if (grabbedRigidbody) return;
        
        GameObject objectToGrab = objectsInRange.ToArray()[0];
        if(objectToGrab.TryGetComponent(out grabbable))
        {
            objectToGrab.transform.SetParent(transform);
            grabbedRigidbody = objectToGrab.GetComponent<Rigidbody>();
            grabbedRigidbody.isKinematic = true;
            grabbedRigidbody.velocity = Vector3.zero;
            grabbable.SetGrabbedState(true, this);
        }  
    }

    public void Release()
    {
        if (grabbedRigidbody == null) return;

        grabbedRigidbody.transform.SetParent(null);
        grabbedRigidbody.isKinematic = false;
        grabbedRigidbody = null;
        grabbable.SetGrabbedState(false, this);
        grabbable = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInRange.Contains(other.gameObject))
        {
            objectsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInRange.Contains(other.gameObject))
        {
            objectsInRange.Remove(other.gameObject);
        }
    }

    public void ResetGrabbedRigidbody()
    {
        grabbedRigidbody = null;
    }
}
