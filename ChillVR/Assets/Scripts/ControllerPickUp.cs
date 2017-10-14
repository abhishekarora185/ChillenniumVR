using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ControllerPickUp : MonoBehaviour
{

    public Rigidbody attachPoint;

    private SteamVR_TrackedObject attachedTrackedObject;
    private SteamVR_Controller.Device device;
    private FixedJoint attachedObjectJoint;

    private void Awake()
    {
        this.attachedObjectJoint = null;
        this.attachedTrackedObject = this.gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider collider)
    {
        this.device = SteamVR_Controller.Input((int)attachedTrackedObject.index);
        if (attachedObjectJoint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (collider.gameObject.GetComponent<Rigidbody>() != null)
            {
                attachedObjectJoint = collider.gameObject.AddComponent<FixedJoint>();
                attachedObjectJoint.connectedBody = attachPoint;
            }
        }
    }

    private void FixedUpdate()
    {
        this.device = SteamVR_Controller.Input((int)attachedTrackedObject.index);
        if (attachedObjectJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject objectToUpdate = attachedObjectJoint.gameObject;
            DestroyImmediate(attachedObjectJoint);
            attachedObjectJoint = null;

            Transform origin = (attachedTrackedObject.origin) ? attachedTrackedObject.origin : attachedTrackedObject.transform.parent;

            if (origin != null)
            {
                Vector3 velocity = device.velocity;
                velocity.x *= -1;
                velocity.z *= -1;
                objectToUpdate.GetComponent<Rigidbody>().velocity = velocity;
                objectToUpdate.GetComponent<Rigidbody>().angularVelocity = origin.TransformVector(device.angularVelocity);
            }
            else
            {
                Vector3 velocity = device.velocity;
                velocity.x *= -1;
                velocity.z *= -1;
                objectToUpdate.GetComponent<Rigidbody>().velocity = velocity;
                objectToUpdate.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
            }

            objectToUpdate.GetComponent<Rigidbody>().maxAngularVelocity = objectToUpdate.GetComponent<Rigidbody>().angularVelocity.magnitude;
        }
    }
}
