using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Transform player;
    public bool homing; //Give the bullet a slight homing property?
    bool homedOnAlready = false; //Don't add homing force if already homes on before.

    public Rigidbody rb;
    public float speed;
    public float explosionForce;
    public float explosionRadius;
    public float time;
    public GameObject explosionFX;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (homing && !homedOnAlready)
        {
            homedOnAlready = true;
            transform.LookAt(player.position + new Vector3(0, 1.3f, 0));
        }

        if (rb.velocity.sqrMagnitude < speed * speed)
        {
            if (homing)
                rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
            else
                rb.AddRelativeForce(Vector3.up * speed, ForceMode.Force);
        }

        if (timer >= time)
            Destroy(gameObject);

    }

    void OnCollisionEnter(Collision other)
    {

        rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        //Play particle explosion effect at position
        Instantiate(explosionFX, transform.position, transform.rotation);
        //StartCoroutine(Destroy());
        Destroy(gameObject);
    }

    //IEnumerator Destroy() {
    //    yield return new WaitForEndOfFrame();
    //    Destroy(gameObject);
    //}

}
