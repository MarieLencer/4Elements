using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject multiplayerSelection;
    [SerializeField] private GameObject playPanel;
    [SerializeField] private TMP_InputField inputField;
    public GameSettings settings;
    public int seed;

    private void Start()
    {
        seed = UnityEngine.Random.Range(0, Int32.MaxValue);
        settings = FindObjectOfType<GameSettings>();
        SetSeedInput();
        OnSeedInput();
    }

    public void OnPlaySelect ()
    {
        multiplayerSelection.SetActive(true);
        playPanel.SetActive(false);
    }

    public void OnSelectOnePlayer()
    {
        settings.SetNumPlayers(1);
    }
    
    public void OnSelectTwoPlayers()
    {
        settings.SetNumPlayers(2);
    }
    
    public void OnSelectThreePlayers()
    {
        settings.SetNumPlayers(3);   
    }
    
    public void OnSelectFourPlayers()
    {
        settings.SetNumPlayers(4);
    }

    public void OnStartGame ()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void SetSeedInput()
    {
        inputField.text = "" + seed;
    }

    public void OnSeedInput()
    {
        seed = Int32.Parse(inputField.text);
        settings.setSeed(seed);
    }
}
