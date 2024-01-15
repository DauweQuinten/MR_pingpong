using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DirectInteractorWithControllers : MonoBehaviour
{
    private List<GameObject> objectsInRange = new List<GameObject>();
    private GrabbableObjectWithControllers grabbable = null;
    private Vector3 controllerVelocity = Vector3.zero;
    private Vector3 controllerPrevPos = Vector3.zero;
    [SerializeField, Tooltip("Multiplier of force of throw when released")] float velocityMultiplier = 1f;

    public void Grab()
    {
        if (grabbable || objectsInRange.Count == 0) return;
        
        GameObject objectToGrab = objectsInRange.ToArray()[0];
        if(objectToGrab.TryGetComponent(out grabbable))
        {
            grabbable.SetGrabbedState(true, this);
            grabbable.Grab();       
        }  
    }

    public void Release()
    {
        if (grabbable == null) return;
        grabbable.SetGrabbedState(false, this);
        grabbable.Release(controllerVelocity * velocityMultiplier);
        grabbable = null;
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

    public void ResetGrabbable()
    {
        grabbable = null;
    }
}
