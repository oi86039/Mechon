using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObj : MonoBehaviour
{
    public OVRGrabber LeftGrabber;
    public Transform LeftAnchor;
    public OVRGrabber RightGrabber;
    public Transform RightAnchor;
    OVRGrabbable grabbable;

    public GameObject hologram; //Show little hologram of where destination is
    public Transform snapDestination; //Destination of where snap should end up

    public float range;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        //If grabbing object
        if (grabbable.grabbedBy == LeftGrabber)
        {
            Snap(LeftGrabber, LeftAnchor);
        }
        else if (grabbable.grabbedBy == RightGrabber)
        {
            Snap(RightGrabber, RightAnchor);
        }
        //If not grabbed
        else
        {
            //Set hologram off
            hologram.SetActive(false);
        }

    }

    void Snap(OVRGrabber grabber, Transform anchor)
    {
        //Check distance between snapDestination and snapObj
        float distance = Vector3.Distance(anchor.position, snapDestination.position);
        Debug.Log(distance);

        //If too far, show hologram and go to hand position.
        if (distance > range)
        {
            hologram.SetActive(true);
        }

        else
        {
            hologram.SetActive(false);
            //snap
            transform.position = Vector3.Lerp(transform.position, snapDestination.position, 0.2f);
            transform.rotation = Quaternion.Lerp(transform.rotation, snapDestination.rotation, 0.2f);
           
        }
    }

}

