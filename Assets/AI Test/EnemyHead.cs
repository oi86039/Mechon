using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DisablerAi_Implemented;

public class EnemyHead : EnemyJoint
{
    public RobotHead head;

    // Start is called before the first frame update
    void Awake()
    {
        head = new RobotHead(new Location(transform.position));
        Debug.Log("Head set???");
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(coll, other);

            //if (isHead)
            //    SendMessageUpwards("DieNow");
            //else SendMessageUpwards("Die");
        }

        else if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
        { //Shot with bullet or item
            SendMessageUpwards("Shot"); //Player was bodyshot
        }
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(coll, other.collider);

            //if (isHead)
            //    SendMessageUpwards("DieNow");
            //else SendMessageUpwards("Die");
        }
        else if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
        { //Shot with bullet or item
            SendMessageUpwards("Shot"); //Player was bodyshot
        }
    }

}
