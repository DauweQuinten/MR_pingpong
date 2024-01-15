using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DirectInteractorWithControllers : MonoBehaviour
{
    private Rigidbody grabbedRigidbody = null;
    private List<GameObject> objectsInRange = new List<GameObject>();
    private GrabbableObjectWithControllers grabbable = null;
    private Vector3 controllerVelocity = Vector3.zero;
    private Vector3 controllerPrevPos = Vector3.zero;
    [SerializeField, Tooltip("Multiplier of force of throw when released")] float velocityMultiplier = 1f;

    public void Grab()
    {
        if (grabbedRigidbody || objectsInRange.Count == 0) return;
        
        GameObject objectToGrab = objectsInRange.ToArray()[0];
        if(objectToGrab.TryGetComponent(out grabbable))
        {
            grabbable.SetGrabbedState(true, this);

            objectToGrab.transform.SetParent(transform);
            grabbedRigidbody = objectToGrab.GetComponent<Rigidbody>();
            grabbedRigidbody.isKinematic = true;
            grabbedRigidbody.velocity = Vector3.zero;          
        }  
    }

    public void Release()
    {
        if (grabbedRigidbody == null) return;

        //grabbable.SetGrabbedState(false, this);
        //grabbedRigidbody.isKinematic = false;
        //grabbedRigidbody.velocity = controllerVelocity * velocityMultiplier;
        //grabbedRigidbody.transform.SetParent(null);
        //grabbedRigidbody = null;

        grabbable.Release(controllerVelocity * velocityMultiplier);
        grabbable = null;
        grabbedRigidbody = null;
    }


    private void Update()
    {
        controllerVelocity = (transform.position - controllerPrevPos) / Time.deltaTime;
        controllerPrevPos = transform.position;
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
