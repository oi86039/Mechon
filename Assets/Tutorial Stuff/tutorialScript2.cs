using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript2 : MonoBehaviour
{
    public GameObject leftJoystick;

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
