using UnityEngine;

public class EnemyJoint : MonoBehaviour
{
    protected Collider coll;

    // Start is called before the first frame update
    private void Start()
    {
        coll = GetComponent<Collider>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(coll, other);

            //if (isHead)
            //    SendMessageUpwards("DieNow");
            //else SendMessageUpwards("Die");
        }

        else if (other.gameObject.layer == 10 || other.gameObject.layer == 12 || other.gameObject.layer == 9 || other.gameObject.layer == 11)
        { //Shot with bullet or item
            SendMessageUpwards("Shot"); //Player was bodyshot
            Debug.Log("Shot @ " + gameObject.name);
        }
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(coll, other.collider);

            //if (isHead)
            //    SendMessageUpwards("DieNow");
            //else SendMessageUpwards("Die");
        }
        else if (other.gameObject.layer == 10 || other.gameObject.layer == 12)
        { //Shot with bullet or item
            SendMessageUpwards("Shot"); //Player was bodyshot
            Debug.Log("Shot @ " + gameObject.name);
        }
    }
}