using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public Animator scopeAnim;
    public TextMeshProUGUI ammoDisplay;

    public Animator recoil;
    public bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        scopeAnim.SetBool("On", false); //Set On if off, and off if on
        recoil = GetComponent<Animator>();
        ammoDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canFire)
        {
            ammoDisplay.color = Color.red;
            ammoDisplay.text = "0";
        }

        //Draw debug laser pointer
        Debug.DrawRay(bulletSpawnPoint.position, bulletSpawnPoint.up * 50, Color.red);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && canFire)
        {
            Debug.Log("Fire");
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            recoil.SetTrigger("Fire");
            BroadcastMessage("SubtractAmmo");
        }

        //Show Sight
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            bool gunOn = scopeAnim.GetBool("On");
            scopeAnim.SetBool("On", !gunOn); //Set On if off, and off if on
        }
        canFire = false;

    }

    void DisplayAmmo(float ammo) {
        ammoDisplay.color = Color.white;
        ammoDisplay.text = ammo.ToString();
    }

    void CanFire() {
        canFire = true;
    }

    void CannotFire() {
        canFire = false;
    }

}
