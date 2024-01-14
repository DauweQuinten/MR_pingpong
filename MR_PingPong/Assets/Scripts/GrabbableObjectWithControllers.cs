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
        if (_isGrabbed)
        {
            _interactor.ResetGrabbedRigidbody();
        }
        _isGrabbed = isGrabbed;
        _interactor = interactor;
    }
}
