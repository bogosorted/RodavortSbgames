using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneBehaviour : MonoBehaviour
{

    void Update()
    {
        if(Input.anyKey)
        {
         SceneManager.LoadScene("MenuMultiplayer");
         GameObject.FindGameObjectWithTag("MusicaDeFundo").GetComponent<AudioSource>().volume = 0.12f;
        }
        
    }
}
