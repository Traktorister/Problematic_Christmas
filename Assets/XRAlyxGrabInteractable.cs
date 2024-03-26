using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRAlyxGrabInteractable : XRGrabInteractable
{
    public float velocityThreshold = 2;

    private XRRayInteractor rayInteractor;
    private Vector3 previousPos;
    private Rigidbody interactableRigidbody;
    protected override void Awake()
    {
        base.Awake();
        interactableRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(isSelected && firstInteractorSelecting is XRRayInteractor)
        {
            Vector3 velocity = (rayInteractor.transform.position - previousPos) / Time.deltaTime;
            previousPos = rayInteractor.transform.position;

            if(velocity.magnitude > velocityThreshold)
            {
                Drop();
                interactableRigidbody.velocity = Vector3.up;
            }
        }
    }
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject is XRRayInteractor)
        {
            trackPosition = false;
            trackRotation = false;
            throwOnDetach = false;

            rayInteractor = (XRRayInteractor)args.interactorObject;
            previousPos = rayInteractor.transform.position;
        }
        else
        {
            trackPosition = true;
            trackRotation = true;
            throwOnDetach = true;
        }

        base.OnSelectEntered(args);
    }
}