using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkNewHud : MonoBehaviour
{
    public NetworkManager manager;
    PlayerId playerId;
    
    void Awake()
    {
            manager = GetComponent<NetworkManager>();
    }
    public void OnStop()
    {
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerId = ntwrkid.GetComponent<PlayerId>();
        playerId.CmdStopHost();
    }    
}
