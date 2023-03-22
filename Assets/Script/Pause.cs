using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused = false;
    private string input;

    public GameObject pauseMenu;
    public Player Player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }

    void pause()
    {
        if (!paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        paused = !paused;
    }

    public void ChangeSpeed(string speed)
    {
        if(speed != "")
        {
            input = speed;
            Player.setSpeed(Convert.ToSingle(input));
        }
        else
        {
            return;
        }
       
    }
}