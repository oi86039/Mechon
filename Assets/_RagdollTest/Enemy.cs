using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Rigidbody[] jointRigidBodies;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Put this on the player controller/gun instead
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            SceneManager.LoadScene(0);
        }

    }
    void Die()
    {
        foreach (Rigidbody rb in jointRigidBodies)
        {
            rb.isKinematic = false; //Enable Ragdoll


        }

    }

}
