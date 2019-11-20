using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    public GameObject leftJoystick;
    public GameObject leftTrigger;
    public GameObject rightTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "buttonStart")
        {
            leftStickPause();
        }

        //write if statements to determine which tutorial should pop up and be set active
    }

    private void Update()
    {
        //Vector2 x_joy = OVRInput.Axis2D.PrimaryThumbstick;
        //if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) > .5) //if left trigger is used
        //{
          //  leftStickResume();
        //}

    }

    public void leftStickPause()
    {
        leftJoystick.SetActive(true);
    }

    public void leftStickResume()
    {
        leftJoystick.SetActive(false);
    }
}
