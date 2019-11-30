using DisablerAi_Implemented;

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisions : MonoBehaviour
{
    [Header("AI")]
    public Gun disablerScript; /**Disabler obj for ai code*/
    Disabler disabler; /**Disabler obj for ai code*/

    public Player player; /**Player obj for ai code*/

    [Header("Properties")]
    public Animator anim; //Fade animation

    private CharacterController controller;
    public int health = 3;

    private void Awake()
    {
        disabler = new Disabler(new Location(disablerScript.transform.position));
        //Create new player
        player = new Player(disabler, new Location(transform.position));

    }

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Update ai
        player.Disabler.Location = new Location(disablerScript.transform.position);
        player.Location = new Location(transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 15) //15 = enemy bullet
        {
            Debug.Log("PLAYER HIT, AHHHHHHHHH");
            health--;
            anim.SetTrigger("Hurt");
        }
        if (health <= 0)
        {
            health = 0;
            anim.SetBool("Death", true);
            StartCoroutine(Death());
        }
       
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}