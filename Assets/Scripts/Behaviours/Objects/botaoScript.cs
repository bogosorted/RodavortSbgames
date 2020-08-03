using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Discovery;
using Mirror;

public class botaoScript : MonoBehaviour
{
    public ServerResponse Info;
    
    public void OnClick()
    {
        NetworkManager.singleton.StartClient();
    }
    
    public void OnHostClick()
    {
        if (!NetworkClient.active)
        GameObject.Find("NetworkDiscovery").GetComponent<Hud>().HostConnect();
    }
    public void OnFindServerClick()
    {
        Info = null;
        if(!NetworkClient.active)
            GameObject.Find("NetworkDiscovery").GetComponent<Hud>().FindServer();
    }
    public void OnDisconnectServer()
    {
    
        GameObject.Find("NetworkManager").GetComponent<NetworkNewHud>().OnStop();
    }
}
