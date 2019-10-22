using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoslterLocale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //if the ammo is not being grabbed, set the kinematic to true and the parent of the ammo to the hoslter box
        //if (other.gameObject.GetComponent<Ammo>().isGrabbed == false && other.gameObject.tag =="Ammo")
        if (other.gameObject.tag == "Ammo")
        {
            Debug.Log("we touched");
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.transform.parent = this.gameObject.transform;
        }
    }
}
