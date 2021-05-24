using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Configuration;
using System.Net.NetworkInformation;
using MLAPI.Transports.UNET;
using MLAPI.Transports.Tasks;




public class botaoScript : MonoBehaviour
{
    Button botaoHost,botaoCriarSala,botaoEntrarSala;
    GameObject signal;

    void Start()
    {
        botaoHost = GameObject.Find("botaoH").GetComponent<Button>();
        botaoCriarSala = GameObject.Find("CRIAR_SALA").GetComponent<Button>();
        botaoEntrarSala = GameObject.Find("ENTRAR_SALA").GetComponent<Button>();
    }
    bool IsPortFree()
    {
       var isInUsed = IPGlobalProperties.GetIPGlobalProperties().GetActiveUdpListeners().Any(p => p.Port == 7777);
       return !isInUsed;
    }

    public void OnClick()
    {
        if(NetworkManager.Singleton.IsClient)
            NetworkManager.Singleton.StopClient(); 
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            ChangeIpAdress();
            SocketTasks client = NetworkManager.Singleton.StartClient(); 
            if(!client.Success)
            {
                print("algo deu errado!");
            }
        }
    }
    
    public void OnHostClick()
    {
        if(NetworkManager.Singleton.IsClient)
            NetworkManager.Singleton.StopClient();

        if(!IsPortFree())
        {
            print("a porta ja esta sendo usada!");
        }
        else if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer){
        NetworkManager.Singleton.StartHost();
        signal = GameObject.Find("HostSignal");
        signal.GetComponent<Image>().color = new Color32(59,140,72,255);
        botaoHost.interactable = true;
        botaoCriarSala.interactable = false;
        botaoEntrarSala.interactable = false;
        }
    }


    public void OnStopHost()
    {
         if(NetworkManager.Singleton.IsHost)
         {
            NetworkManager.Singleton.StopHost();
            signal = GameObject.Find("HostSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            signal = GameObject.Find("ClientSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            botaoHost.interactable = false;
            botaoCriarSala.interactable = true;
            botaoEntrarSala.interactable = true;
            GameObject.Find("JOGAR").GetComponent<Button>().interactable = false;
         }
    }
    public void StopClient()
    {
        if(NetworkManager.Singleton.IsConnectedClient)
        {
            NetworkManager.Singleton.StopClient();
            signal = GameObject.Find("HostSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            signal = GameObject.Find("ClientSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            GameObject.Find("botaoC").GetComponent<Button>().interactable = false;
            botaoEntrarSala.interactable = true;
            GameObject.Find("JOGAR").GetComponent<Button>().interactable = false;
            GameObject.Find("CRIAR_SALA").GetComponent<Button>().interactable = true;
        }
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene("MenuInicial");
    }
    
    public void ChangeIpAdress()
    {
        string ipAdress =GameObject.Find("InputField").GetComponent<InputField>().text;
        GameObject.Find("NetworkManager").GetComponent<UNetTransport>().ConnectAddress = ipAdress != "" ? ipAdress : "127.0.0.1";
    }
   
}
