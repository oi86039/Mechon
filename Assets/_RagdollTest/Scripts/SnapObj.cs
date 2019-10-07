using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObj : MonoBehaviour
{

    OVRGrabber LeftGrabber;
    Transform LeftAnchor;
    OVRGrabber RightGrabber;
    Transform RightAnchor;
    OVRGrabbable grabbable;

    public bool canSnap;
    public GameObject hologram; //Show little hologram of where destination is
    public Transform snapDestination; //Destination of where snap should end up
    public bool isSnapped; //Is object snapped on or not?
    public bool isGrabbed; //Is object snapped on or not?

    public float grabRange;
    public float snapRange;

    Rigidbody rb;
    Outline outline;

    public Collider[] ignoreColliders;
    Collider thisCollider;

    void Awake()
    {
        LeftGrabber = GameObject.Find("hand_left").GetComponent<OVRGrabber>();
        LeftAnchor = GameObject.Find("LeftHandAnchor").transform;
        RightGrabber = GameObject.Find("hand_right").GetComponent<OVRGrabber>();
        RightAnchor = GameObject.Find("RightHandAnchor").transform;
        grabbable = GetComponent<OVRGrabbable>();

        rb = GetComponent<Rigidbody>();
        outline = gameObject.AddComponent<Outline>();

        thisCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If grabbing object
        if (grabbable.grabbedBy == LeftGrabber)
        {
            isGrabbed = true;
            Snap(LeftGrabber, LeftAnchor);
        }
        else if (grabbable.grabbedBy == RightGrabber)
        {
            isGrabbed = true;
            Snap(RightGrabber, RightAnchor);
        }
        //If not grabbed
        else
        {
            isGrabbed = false;
            //if snapped object is in place, set to kinematic
            if (transform.parent == snapDestination)
            {
                rb.isKinematic = true;
                isSnapped = true;
            }

            //else set to non kinematic
            else
            {
                rb.isKinematic = false;
                isSnapped = false;
            }

            //Set hologram off
            hologram.SetActive(false);

            //Check distance between object and hand
            float distanceLeftHand = Vector3.SqrMagnitude(LeftAnchor.position - transform.position);
            float distanceRightHand = Vector3.SqrMagnitude(RightAnchor.position - transform.position);

            //If within grab range
            if (distanceLeftHand < grabRange * grabRange) //|| distanceRightHand < grabRange * grabRange)
            {
                outline.enabled = true;
                outline.OutlineColor = Color.white;
                outline.OutlineWidth = 6f;
            }
            else
            {
                outline.enabled = false;
            }
        }

    }

    void Snap(OVRGrabber grabber, Transform anchor)
    {
        outline.enabled = true;
        outline.OutlineColor = Color.Lerp(outline.OutlineColor, new Color(0, 0.9137255f, 8332564f), 0.2f);
        outline.OutlineWidth = Mathf.Lerp(outline.OutlineWidth, 10, 0.2f);

        transform.parent = null;

        //Check distance between snapDestination and snapObj
        float distanceObjs = Vector3.SqrMagnitude(anchor.position - snapDestination.position);
        //Debug.Log(distanceObjs);

        //If too far, show hologram and go to hand position.
        if (distanceObjs > snapRange * snapRange)
        {
            hologram.SetActive(true);
            transform.SetParent(null);
        }

        else 
        {
            hologram.SetActive(false);
            //snap
            transform.position = Vector3.Lerp(transform.position, snapDestination.position, 0.15f);
            transform.rotation = Quaternion.Lerp(transform.rotation, snapDestination.rotation, 0.15f);
            transform.localScale = Vector3.Lerp(transform.lossyScale, hologram.transform.lossyScale, 0.15f);

            transform.parent = snapDestination;
            // rb.detectCollisions = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (ignoreColliders.Length > 0)
        {
            for (int i = 0; i < ignoreColliders.Length; i++)
            {
                if (collision.collider == ignoreColliders[i])
                {
                    Physics.IgnoreCollision(collision.collider, thisCollider);
                }
            }

        }
    }


}

