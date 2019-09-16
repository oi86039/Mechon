using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    Rigidbody rb;
    public float speed;
    public float time;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (rb.velocity.sqrMagnitude < speed * speed)
        {
            rb.AddRelativeForce(Vector3.up * speed, ForceMode.Force);
        }

        if (timer >= time)
            Destroy(gameObject);

    }

    void OnCollisionEnter(Collision other)
    {
       // timer = 4.9f;
    }
}
