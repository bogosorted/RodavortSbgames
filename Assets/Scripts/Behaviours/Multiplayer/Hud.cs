using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using UnityEngine.SceneManagement;  

 public class Hud : MonoBehaviour
{     
    public void Sair()
    {
        if(NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StopHost();
            
        }
        else if(NetworkManager.Singleton.IsConnectedClient)
        {
            NetworkManager.Singleton.StopClient();
        }
        SceneManager.LoadScene("MenuMultiplayer");
    }
}