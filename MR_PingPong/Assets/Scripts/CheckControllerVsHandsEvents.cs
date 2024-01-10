using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckControllerVsHandsEvents : MonoBehaviour
{
    public UnityEvent OnHandTrackingStarted;
    public UnityEvent OnTouchControllerActivated;
    public UnityEvent OnNoActiveControllerFound;


    private bool isHandTrackingActive = false;
    private bool isTouchControllerActivated = false;


    // Update is called once per frame
    void Update()
    {
        if (OVRInput.IsControllerConnected(OVRInput.Controller.Hands))
        {
            if (!isHandTrackingActive)
            {
                isHandTrackingActive = true;
                isTouchControllerActivated = false;
                OnHandTrackingStarted.Invoke();
            }
        }
        else if (OVRInput.IsControllerConnected(OVRInput.Controller.Touch))
        {
            if (!isTouchControllerActivated)
            {
                isTouchControllerActivated = true;
                isHandTrackingActive = false;
                OnTouchControllerActivated.Invoke();
            }
        }
        else
        {
            if(isTouchControllerActivated || isHandTrackingActive)
            {
                isTouchControllerActivated = false;
                isHandTrackingActive = false;
                OnNoActiveControllerFound.Invoke();
            }
        }


    }
}
