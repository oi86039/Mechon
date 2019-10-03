using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    // GIP= GameObject is paused
    public static bool GIP = false;

    //For normal pause menu
    public GameObject PausemenuUI;
    //For the "Are you sure" menu for restart checkpoint
    public GameObject PausemenuUI2;
    //For the "Are you sure" menu for the startpoint
    public GameObject PausemenuUI3;
    //For the "Are you sure" menu for the startpoint
    public GameObject PausemenuUI4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GIP)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        GIP = false;
    }
    public void Resume2()
    {
        PausemenuUI2.SetActive(false);
        Time.timeScale = 1f;
        GIP = false;
    }

    public void Resume3()
    {
        PausemenuUI3.SetActive(false);
        Time.timeScale = 1f;
        GIP = false;
    }
    public void Resume4()
    {
        PausemenuUI4.SetActive(false);
        Time.timeScale = 1f;
        GIP = false;
    }
    void Pause()
    {
        PausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        GIP = true;
    }

    public void Pause2()
    {
        PausemenuUI2.SetActive(true);         
        Time.timeScale = 0f;
    //    GIP = true;
    }

    public void Pause3()
    {
       PausemenuUI3.SetActive(true);
       Time.timeScale = 0f;
     //   GIP = true;
    }

    public void Pause4()
    {
        PausemenuUI4.SetActive(true);
        Time.timeScale = 0f;
        //   GIP = true;
    }

    public void Loadmenu()
    {
        //This timescale is to make sure the game doesnt stay paused
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
        //SceneManager.LoadScene("Name of the scene");
    }

    public void checkpoint()
    {
        Time.timeScale = 1f;
        Debug.Log("CheckPoint");
        //SceneManager.LoadScene("Name of the scene");
    }

    public void Startpoint()
    {
        Time.timeScale = 1f;
        Debug.Log("Starting Point");
        //SceneManager.LoadScene("Name of the scene");
    }
    

    public void Quitgame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}
