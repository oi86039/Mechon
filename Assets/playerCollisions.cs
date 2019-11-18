using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DisablerAi;
using DisablerAi.Interfaces;

public class UnityPlayer : IPlayer
{

    GameObject gameObject; /**GameObject this class is attached to*/

    public IDisabler Disabler { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public ILocation Location { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public List<IItem> NearestItems()
    {
        throw new System.NotImplementedException();
    }

    public List<IRobot> NearestRobots()
    {
        throw new System.NotImplementedException();
    }

    public UnityPlayer(GameObject gameObject)
    {
        this.gameObject = gameObject;
    }

}

public class playerCollisions : MonoBehaviour
{
    public UnityPlayer playerScript;
    CharacterController controller;
    public int health = 3;
    public Animator anim; //Fade animation

    // Start is called before the first frame update
    void Start()
    {
        playerScript = new UnityPlayer(gameObject);
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
