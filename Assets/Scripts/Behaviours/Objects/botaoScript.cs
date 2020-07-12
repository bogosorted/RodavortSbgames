using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Discovery;

public class botaoScript : MonoBehaviour
{
    public ServerResponse Info;
    public void OnClick()
    {
        transform.parent.GetComponent<Hud>().Connect(Info);
    }
    public void OnHostClick()
    {
        GameObject.Find("NetworkDiscovery").GetComponent<Hud>().HostConnect();
    }
    public void OnFindServerClick()
    {
        GameObject.Find("NetworkDiscovery").GetComponent<Hud>().FindServer();
    }
    public void OnDisconnectServer()
    {
        GameObject.Find("NetworkManager").GetComponent<NetworkNewHud>().OnStop();
    }
}
