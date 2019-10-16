using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisablerAi;
using DisablerAi.Interfaces;

public class Enemy : MonoBehaviour, IRobot
{

    [Header("IRobot")]
    private Vector3 location; public Vector3 Location
    {
        get { return location; }
        set        {            location = transform.position;        }    }

    [Header("Misc.")]
    public Rigidbody[] jointRigidBodies;
    public RobotAi ai; //Represents Ai of the robot

    // Start is called before the first frame update
    void Start()
    {
        //Init body
        jointRigidBodies = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }
    void Die()
    {
        foreach (Rigidbody rb in jointRigidBodies)
        {
            rb.isKinematic = false; //Enable Ragdoll
        }

    }

    #region IRobot

    #endregion

}

