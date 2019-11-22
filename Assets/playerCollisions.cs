using DisablerAi_Implemented;

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisions : MonoBehaviour
{
    [Header("AI")]
    public Disabler disabler; /**Disabler obj for ai code*/

    private Player player; /**Player obj for ai code*/

    [Header("AI")]
    public Animator anim; //Fade animation

    private CharacterController controller;
    public int health = 3;

    // Start is called before the first frame update
    private void Start()
    {
        player = new Player(disabler, new Location(transform.position));
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Update ai
        player.Location = new Location(transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("PLAYER HIT, AHHHHHHHHH");
            health--;
        }
        if (health <= 0)
        {
            health = 0;
            anim.SetBool("Death", true);
            StartCoroutine(Death());
        }
        else
        {
            anim.SetTrigger("Hurt");
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}