using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Rigidbody[] jointRigidBodies;
    public Animator anim;
    public bool dead; //Is the enemy ragdoll/dead?

    // Start is called before the first frame update
    void Start()
    {
        jointRigidBodies = GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
        dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            anim.enabled = false;

        }
        dead = true;

    }

}
