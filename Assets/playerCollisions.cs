using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCollisions : MonoBehaviour
{

    public CharacterController controller;
    public int health = 3;
    public Animator anim; //Fade animation

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
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

    IEnumerator Death() {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
