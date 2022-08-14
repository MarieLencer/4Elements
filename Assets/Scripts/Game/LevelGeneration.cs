using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] lightedTerrains;

    public GameObject[] nonLightedTerrains;

    private GameObject nextTerrain;

    public GameObject levelTerrain;

    public BuildNavMesh navMesh;

    private Vector2[] skippedPositions = {
        new Vector2(-30f, 0f),
        new Vector2(-30f, 7.5f),
        new Vector2(-22.5f, 0f),
        new Vector2(-22.5f, 7.5f),
        new Vector2(15f, 0f),
        new Vector2(15f, 7.5f),
        new Vector2(22.5f, 0f),
        new Vector2(22.5f, 7.5f),
        new Vector2(-30f, 45f),
        new Vector2(-30f, 52.5f),
        new Vector2(-22.5f, 45f),
        new Vector2(-22.5f, 52.5f),
        new Vector2(15f, 45f),
        new Vector2(15f, 52.5f),
        new Vector2(22.5f, 45f),
        new Vector2(22.5f, 52.5f),
    };

    private int numberLighted = 0;

    private int maxLighted = 15;

    private int arraySize = 8;

    private int[,] lightArray;
    private GameSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        GameObject settingsObject = GameObject.Find("GameSettings");
        settings = settingsObject.GetComponent<GameSettings>();
        UnityEngine.Random.InitState(settings.getSeed());
        lightArray = new int[arraySize, arraySize];
        fillLightArray();
        placeTerrains();
        navMesh.startBuild();
    }

    private void placeTerrains()
    {
        float xPosition = -30f;
        for (int x = 0; x < arraySize; x++)
        {
            float zPosition = 0f;
            for (int z = 0; z < arraySize; z++)
            {
                Vector2 position = new Vector2(xPosition, zPosition);
                if ((xPosition <= -22.5f || xPosition >= 15f) && (zPosition <= 7.5f || zPosition >= 45))
                {
                    zPosition += 7.5f;
                    continue;
                }
                    
                bool lighted = chooseLighted(x, z);
                int terrainIndex = chooseTerrainIndex();
                nextTerrain = lighted ? lightedTerrains[terrainIndex] : nonLightedTerrains[terrainIndex];
                Instantiate(nextTerrain, new Vector3(position.x, 0f, position.y), Quaternion.identity, levelTerrain.transform);
                zPosition += 7.5f;
            }

            xPosition += 7.5f;
        }
    }

    private bool chooseLighted(int x, int z)
    {
        int lighted = UnityEngine.Random.Range(0, 2);
        if (lighted == 1)
        {
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= arraySize)
                    continue;
                for (int j = z - 1; j <= z + 1; j++)
                {
                    if (j < 0 || j >= arraySize)
                        continue;
                    if (lightArray[i, j] == 1)
                    {
                        int newLightWeight = UnityEngine.Random.Range(0, 3);
                        lighted = newLightWeight > 0 ? 1 : 0;
                    }
                }
            }
        }

        lightArray[x, z] = lighted;
        return lighted == 1;
    }

    private void fillLightArray()
    {
        for (int x = 0; x < arraySize; x++)
        {
            for (int y = 0; y < arraySize; y++)
            {
                if((x < 2 || x > 6) && (y < 2 || y > 6))
                {
                    lightArray[x, y] = -1;
                }
                else
                {
                    lightArray[x, y] = 0;
                }
            }
        }
    }

    private int chooseTerrainIndex()
    {
        int index = UnityEngine.Random.Range(0, lightedTerrains.Length + 4);
        if (index >= lightedTerrains.Length)
            index = 0;
        return index;
    }
}
