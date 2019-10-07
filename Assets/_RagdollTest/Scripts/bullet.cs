using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

   public Rigidbody rb;
    public float speed;
    public float explosionForce;
    public float explosionRadius;
    public float time;
    public GameObject explosionFX;

    float timer;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
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
        
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        //Play particle explosion effect at position
        Instantiate(explosionFX,transform.position, transform.rotation);
        //StartCoroutine(Destroy());
        Destroy(gameObject);
    }

    //IEnumerator Destroy() {
    //    yield return new WaitForEndOfFrame();
    //    Destroy(gameObject);
    //}

}
