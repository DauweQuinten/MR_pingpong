using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class GrabbableObjectWithControllers : MonoBehaviour
{
    private bool _isGrabbed;

    public void SetGrabbedState(bool isGrabbed)
    {
        _isGrabbed = isGrabbed;
    }
}
