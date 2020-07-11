using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror.Discovery;
using Mirror;


public class Hud : MonoBehaviour
{
        public GameObject botao;
        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        Vector2 scrollViewPos = Vector2.zero;

        public NetworkDiscovery networkDiscovery;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (networkDiscovery == null)
            {
                networkDiscovery = GetComponent<NetworkDiscovery>();
                UnityEditor.Events.UnityEventTools.AddPersistentListener(networkDiscovery.OnServerFound, OnDiscoveredServer);
                UnityEditor.Undo.RecordObjects(new Object[] { this, networkDiscovery }, "Set NetworkDiscovery");
            }
        }
#endif

        public void Connect(ServerResponse info)
        {
            NetworkManager.singleton.StartClient(info.uri);
        }
        public void HostConnect()
        {
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
        }
        public void FindServer()
        {
                discoveredServers.Clear();
                networkDiscovery.StartDiscovery();
                StartCoroutine(Descobertas());
        }
        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }   
    IEnumerator Descobertas()
    {
        yield return new WaitForSeconds(2);
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
            Destroy(transform.GetChild(i).gameObject);
        foreach (ServerResponse info in discoveredServers.Values)
        {
            GameObject refBotao = Instantiate(botao);
            refBotao.transform.SetParent(transform,false);
            refBotao.GetComponent<botaoScript>().Info = info;
        }
    }

}
