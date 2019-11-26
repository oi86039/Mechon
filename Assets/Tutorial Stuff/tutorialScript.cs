using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    public GameObject rightTrigger;
    //public GameObject backRightTrigger;
    public GameObject aimForHead;
    public GameObject scopes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "pickup")
        {
            rightTrigger.SetActive(true);
            Destroy(other);
        }

        //if (other.tag == "shooting")
        //{
        //    backRightTrigger.SetActive(true);
        //    Destroy(other);
        //}

        if (other.tag == "aiming")
        {
            aimForHead.SetActive(true);
            Destroy(other);
        }
    }

    private void Update()
    {
        //pickup gun
        float trigger = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        if (trigger > 0)
        {
            rightTrigger.SetActive(false);
            scopes.SetActive(true);
        }

        //shooting the gun/aiming for the head
        float backTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if (backTrigger > 0)
        {
            aimForHead.SetActive(false);
        }

        //activating your scope
        bool buttonB = OVRInput.Get(OVRInput.Button.Two);
        if (buttonB == true)
        {
            scopes.SetActive(false);
            Destroy(scopes);
        }
    }
}
