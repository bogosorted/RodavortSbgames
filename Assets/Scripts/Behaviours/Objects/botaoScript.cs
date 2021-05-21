using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MLAPI;

public class botaoScript : MonoBehaviour
{
    // public ServerResponse Info;
    
    public void OnClick()
    {
        // NetworkManager.singleton.networkAddress = GameObject.Find("IpAddress").GetComponent<Text>().text;
        // NetworkManager.singleton.StartClient();
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            NetworkManager.Singleton.StartClient();
    }
    
    public void OnHostClick()
    {
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        NetworkManager.Singleton.StartHost();
        // if (!NetworkClient.active)
        // GameObject.Find("NetworkDiscovery").GetComponent<Hud>().HostConnect();
    }
    public void OnFindServerClick()
    {
        // Info = null;
        // if(!NetworkClient.active)
        //     GameObject.Find("NetworkDiscovery").GetComponent<Hud>().FindServer();
    }
    public void OnDisconnectServer()
    {
        // GameObject.Find("NetworkManager").GetComponent<NetworkNewHud>().OnStop();
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}
