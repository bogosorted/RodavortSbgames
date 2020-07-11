using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror.Discovery;
using Mirror;


public class Hud : MonoBehaviour
{
      public GameObject botao;
        float tempoProcura;
        void Awake(){
            tempoProcura = Time.time + 5;
        }
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

        void OnGUI()
        {
            if (NetworkManager.singleton == null)
                return;

            if (NetworkServer.active || NetworkClient.active)
                return;

            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
                DrawGUI();
        }

        void DrawGUI()
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Find Servers"))
            {
                discoveredServers.Clear();
                networkDiscovery.StartDiscovery();
            }

            // LAN Host
            if (GUILayout.Button("Start Host"))
            {
                discoveredServers.Clear();
                NetworkManager.singleton.StartHost();
                networkDiscovery.AdvertiseServer();
            }

            // Dedicated server
            if (GUILayout.Button("Start Server"))
            {
                discoveredServers.Clear();
                NetworkManager.singleton.StartServer();

                networkDiscovery.AdvertiseServer();
            }

            GUILayout.EndHorizontal();

            // show list of found server

            GUILayout.Label($"Discovered Servers [{discoveredServers.Count}]:");

            // servers
            if(Time.time > tempoProcura)
            {
                int children = transform.childCount;
                for (int i = 0; i < children; ++i)
                    Destroy(transform.GetChild(i).gameObject);
                foreach (ServerResponse info in discoveredServers.Values)
                {
                    GameObject refBotao = Instantiate(botao);
                    refBotao.transform.SetParent(transform,false);
                    refBotao.GetComponent<botaoScript>().Info = info;
                }
                tempoProcura += 5;
            }

        }

        public void Connect(ServerResponse info)
        {
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }
}
