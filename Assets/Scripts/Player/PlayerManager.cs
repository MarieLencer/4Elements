using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private GameSettings settings;

    public GameObject[] players;

    private int playeMaxLife = 50;

    private int numDeactivatedPlayers = 0;

    public GameOverScreen gOScreen;
  
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnStartScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnStartScene;
    }

    void OnStartScene(Scene scene, LoadSceneMode mode)
    {
        GameObject settingsObject = GameObject.Find("GameSettings");
        settings = settingsObject.GetComponent<GameSettings>();
        Debug.Log(settings.GetNumPlayers());
        setPlayers(settings.GetNumPlayers());
    }
    
    private void setPlayers(int numOfPlayers)
    {
        Debug.Log(numOfPlayers);
        for (int i = 0; i < players.Length; i++)
        {
            if (i < numOfPlayers)
            {
                players[i].GetComponent<PlayerMovement>().SetEnabled(true);
            }
            else
            {
                players[i].GetComponent<PlayerMovement>().SetEnabled(false);
            }
        }
    }

    public int getPlayerMaxLife()
    {
        return playeMaxLife;
    }

    public void deactivatePlayer()
    {
        numDeactivatedPlayers++;
        if (numDeactivatedPlayers == settings.GetNumPlayers())
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gOScreen.Activate();
    }
}
