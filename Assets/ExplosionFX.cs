using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFX : MonoBehaviour
{
    float time;
    public float timer;
    public Rigidbody rb;
    public float explosionForce;
    public float explosionRadius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);


        time += Time.deltaTime;

        if (time >= timer) {
            Destroy(gameObject);
        }
    }
}
