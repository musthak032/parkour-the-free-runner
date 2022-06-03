using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class uimanager : MonoBehaviour
{
    [Header("gameve over")]
    public GameObject gameover;
    bool isgameover=false;

    [Header("player ref")]

    public playercontrol player;

    public GameObject futurebox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            showgameover();
        }
       

    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }
    public void exit() {

        Application.Quit();
    }

    public void shop()
    {
        futurebox.SetActive(true);
        Invoke("offfuturebox", 1f);
    }
    void offfuturebox()
    {
        futurebox.SetActive(false);

    }
    public void showgameover()
    {

        if (player.playerdeath&&!isgameover)
        {
            gameover.SetActive(true);
            isgameover = true;


        }
     
    }

    public void restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
