using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonCommands : MonoBehaviour
{
	
	public GameObject mainButton;
    public GameObject mechonLogo;

	public Animator anime;
    public GameObject tutorialStart;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        Vector2 x_joy = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (x_joy.x != 0 || x_joy.y != 0 ) //if left trigger is used
        {
            tutorialStart.SetActive(false);
        }
    }

    public void Settings() {
	}

	public void Exit() {
		Application.Quit();
	}
	
	public void startGame() {
		SceneManager.LoadScene("insert scene name here");
	}
	
	public void Credits() {
		SceneManager.LoadScene("insert scene name here");
	}
	
	public void pressMe() {
        anime.SetTrigger("ifPressed");
        tutorialStart.SetActive(true);

	}

    public void OnTriggerEnter(Collider other)
    {
        pressMe();
    }

}