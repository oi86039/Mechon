using DisablerAi_Implemented;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{

    //  public Disabler disabler;                      /**Disabler component for AI code*/
    public GameObject player;
    public GameObject bullet;
    public ParticleSystem shootFX;
    public Transform bulletSpawnPoint;
    public Animator scopeAnim;
    public TextMeshProUGUI ammoDisplay;

    //  public GameObject camera;

    public Animator recoil;
    public bool canFire;

    private void Awake()
    {
        //Init AI
      //  disabler = new Disabler(new Location(transform.position));
    }

    // Start is called before the first frame update
    private void Start()
    {
        scopeAnim.SetBool("On", false); //Set On if off, and off if on
        recoil = GetComponent<Animator>();
        ammoDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Update ai
       // disabler.Location = new Location(transform.position);

        //Restart on start button
        //Put this on the player controller/gun instead
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            SceneManager.LoadScene(0);
        }

        if (!canFire)
        {
            ammoDisplay.color = Color.red;
            ammoDisplay.text = "0";
        }

        //Draw debug laser pointer
        Debug.DrawRay(bulletSpawnPoint.position, bulletSpawnPoint.up * 50, Color.red);

        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && canFire)
        {
            if (recoil.GetBool("StayOpen") == false)
            {
                Debug.Log("Fire");
                var b = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet bb = b.GetComponent<bullet>();
                bb.player = player;
                bb.homing = false;
                recoil.SetTrigger("Fire");
                shootFX.Play();
                BroadcastMessage("SubtractAmmo");
            }
        }

        //Show Sight
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            bool gunOn = scopeAnim.GetBool("On");
            scopeAnim.SetBool("On", !gunOn); //Set On if off, and off if on
        }

        //Move Camera Shot if placed
        //  Vector2 CameraMov = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //  camera.transform.Rotate(Vector3.left * CameraMov.y);
        //  camera.transform.Rotate(Vector3.up * CameraMov.x);
        canFire = false;

        //Open Disabler if button is held down
        if (OVRInput.Get(OVRInput.Button.One))
        {
            // recoil.SetTrigger("Open");
            recoil.SetBool("StayOpen", true);
        }
        //if (OVRInput.GetUp(OVRInput.Button.One))
        else
        {
            recoil.SetBool("StayOpen", false);
            //recoil.SetTrigger("Close");
        }
        //Close Disabler once button is not held down
    }

    private void DisplayAmmo(float ammo)
    {
        ammoDisplay.color = Color.white;
        ammoDisplay.text = ammo.ToString();
    }

    private void CanFire()
    {
        canFire = true;
    }

    private void CannotFire()
    {
        canFire = false;
    }
}