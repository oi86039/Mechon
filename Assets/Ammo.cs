using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : SnapObj
{
   // public TextMeshProUGUI ammoDisplay;
    public int ammo;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isSnapped && !isGrabbed && ammo > 0)
        {
            SendMessageUpwards("DisplayAmmo", ammo);
            SendMessageUpwards("CanFire");
           
        }
        //else {
        //    ammoDisplay.text = "0";
        //    ammoDisplay.color = Color.red;
        //}
    }

    void SubtractAmmo() {
        ammo--;
    }

}
