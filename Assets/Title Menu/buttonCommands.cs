using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonCommands : MonoBehaviour
{
	
	public GameObject mainButton;
    public GameObject mechonLogo;

	public Animator anime;
	
    // Start is called before the first frame update
    void Start()
    {
        
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
	}

    public void OnTriggerEnter(Collider other)
    {
        pressMe();
    }

}