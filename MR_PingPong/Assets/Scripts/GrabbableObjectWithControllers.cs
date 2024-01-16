using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GrabbableObjectWithControllers : MonoBehaviour
{
    private bool _isGrabbed;
    private DirectInteractorWithControllers _interactor;

    public void SetGrabbedState(bool isGrabbed, DirectInteractorWithControllers interactor)
    {
        if (_isGrabbed && isGrabbed)
        {
            _interactor.ResetGrabbable();
        }
        _isGrabbed = isGrabbed;
        _interactor = interactor;
    }

    public void Grab()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetParent(_interactor.transform);
    }

    public void Release(Vector3 releaseVelocity)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = releaseVelocity;
        rb.transform.SetParent(null);
    }
}
