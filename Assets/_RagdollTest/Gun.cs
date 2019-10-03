using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public Animator scopeAnim;

    // Start is called before the first frame update
    void Start()
    {
        scopeAnim.SetBool("On", false); //Set On if off, and off if on

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Draw debug laser pointer
        Debug.DrawRay(bulletSpawnPoint.position, bulletSpawnPoint.up * 50, Color.red);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Fire");
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }

        //Show Sight
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            bool gunOn = scopeAnim.GetBool("On");
            scopeAnim.SetBool("On", !gunOn); //Set On if off, and off if on
        }
    }
}
