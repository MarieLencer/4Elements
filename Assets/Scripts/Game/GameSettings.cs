using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public int numberPlayers;

    private int seed;
    
    private static GameSettings settingsInstance;
    

    void Awake()
    {
        Application.targetFrameRate = 300;
        if (settingsInstance == null)
        {
            settingsInstance = this;
            DontDestroyOnLoad(this);
        }
        else if (settingsInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetNumPlayers(int num)
    {
        numberPlayers = num;
    }

    public int GetNumPlayers()
    {
        return numberPlayers;
    }

    public void setSeed(int newSeed)
    {
        seed = newSeed;
    }
    public int getSeed()
    {
        return seed;
    }
}
