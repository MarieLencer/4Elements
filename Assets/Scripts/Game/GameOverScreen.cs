using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject panel;
    public void Activate()
    {
        panel.SetActive(true);
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
