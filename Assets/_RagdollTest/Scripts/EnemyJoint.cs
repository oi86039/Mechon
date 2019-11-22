using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisablerAi_Implemented;

public class EnemyJoint : MonoBehaviour
{
    public bool isHead; //If head hit, kill robot immediately
    public RobotHead head; //Only set if this joint is the head of the robot

    // Start is called before the first frame update
    void Start()
    {
        if (isHead)
        {
            head = new RobotHead(new Location(transform.position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Update location
        head.Location = new Location(transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Player"))
        {
            if (isHead) { head.Shot = true; } //Player was headshot

            else SendMessageUpwards("Shot"); //Player was bodyshot
            //if (isHead)
            //    SendMessageUpwards("DieNow");
            //else SendMessageUpwards("Die");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (!gameObject.CompareTag("Player"))
        {
            if (isHead) { head.Shot = true; } //Player was headshot

            else SendMessageUpwards("Shot"); //Player was bodyshot

            //if (isHead)
            //    SendMessageUpwards("DieNow");
            //else SendMessageUpwards("Die");
        }
    }
}
