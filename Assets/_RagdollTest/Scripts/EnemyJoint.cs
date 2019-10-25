using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJoint : MonoBehaviour
{
    public bool isHead; //If head hit, kill robot immediately

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (isHead)
            SendMessageUpwards("DieNow");
        else SendMessageUpwards("Die");
    }

    void OnCollisionEnter(Collision other)
    {
        if (isHead)
            SendMessageUpwards("DieNow");
        else SendMessageUpwards("Die");
    }
}
