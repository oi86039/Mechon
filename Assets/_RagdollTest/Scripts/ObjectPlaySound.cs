using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaySound : MonoBehaviour
{
    public AudioSource source;
    public LayerMask targetMask;
    public float viewRadius;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    [SerializeField]
    private bool isAudObjFound;

    void Start()
    {
        StartCoroutine("FindAudTargetsWithDelay", .2f);
    }

    IEnumerator FindAudTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            CreateListenArea();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Floor")
        {
            Debug.Log("sound triggered");
            Debug.Log(visibleTargets.Count);
            //would play the sound here
            //source.Play();           
            if (visibleTargets.Count == 0)
            {
                //if there are no objs in the range do nothing
                isAudObjFound = false;
            }
            else
            {
                //otherwise get the first enemy
                //this isAudObjFound basically means if the aud obj is heard by a enemy
                isAudObjFound = true;
                Debug.Log("Found enemy: "+visibleTargets[0].root.name);
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        isAudObjFound = false;
    }

    void CreateListenArea()
    {
        //creates a area around the sound obj that the enemy needs to be in order to be within range to hear it
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget))
            {
                visibleTargets.Add(target);
                Debug.Log("audio taget within space");
            }            
        }
    }
}
