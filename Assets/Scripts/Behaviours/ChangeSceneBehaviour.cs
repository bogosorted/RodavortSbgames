using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBehaviour : MonoBehaviour
{
    public float volumeMusicTheme = 0.12f;
    public void OnLanMultiplayerClick()
    {
         ConfigurationMusicTheme();
         SceneManager.LoadScene("MenuMultiplayer");
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Creditos");
        ConfigurationMusicTheme();
    }
    void ConfigurationMusicTheme()
    {
        GameObject.FindGameObjectWithTag("MusicaDeFundo").GetComponent<AudioSource>().volume = volumeMusicTheme;
    }
}
