using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ExitGame : MonoBehaviour
{
    private FPSLog log;
    public GameObject exitScreen;
    private bool activeScreen = false;
    void Start()
    {
        log = gameObject.GetComponent<FPSLog>();
        if (!log)
        {
            Debug.Log("FPSLog not found!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!activeScreen)
            {
                exitScreen.SetActive(true);
                Time.timeScale = 0f;
                activeScreen = true;
            }
            else
            {
                exitScreen.SetActive(false);
                Time.timeScale = 1f;
                activeScreen = false;
            }
        }
    }
    
    public void onExitGame()
    {
        if (log.WriteData())
        {
            
            Application.Quit();
        }
    }
}
