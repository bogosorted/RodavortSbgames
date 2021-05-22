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
                //msg de erro de digitação do usuario no ipaddress
            }
        }
    }
    
    
    public void FailToConnect(){
        print ("connection error");
      
    }
    
    public void OnHostClick()
    {
        if(NetworkManager.Singleton.IsClient)
            NetworkManager.Singleton.StopClient();

        if(!IsPortFree())
        {
            //PRINTAR "ALGUEM JA ESTA HOSTEANDO NA REDE" PRO JOGADOR VISUALIZAR
        }
        else if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer){
        NetworkManager.Singleton.StartHost();
        GameObject signal = GameObject.Find("HostSignal");
        signal.GetComponent<Image>().color = new Color32(59,140,72,255);
        GameObject.Find("botaoH").GetComponent<Button>().interactable = true;
        GameObject.Find("CRIAR_SALA").GetComponent<Button>().interactable = false;
        GameObject.Find("ENTRAR_SALA").GetComponent<Button>().interactable = false;
        }
    }


    public void OnStopHost()
    {
         if(NetworkManager.Singleton.IsHost)
         {
            NetworkManager.Singleton.StopHost();
            GameObject signal = GameObject.Find("HostSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            signal = GameObject.Find("ClientSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            GameObject.Find("botaoH").GetComponent<Button>().interactable = false;
            GameObject.Find("CRIAR_SALA").GetComponent<Button>().interactable = true;
            GameObject.Find("ENTRAR_SALA").GetComponent<Button>().interactable = true;
         }
    }
    public void StopClient()
    {
        if(NetworkManager.Singleton.IsConnectedClient)
        {
            NetworkManager.Singleton.StopClient();
            GameObject signal = GameObject.Find("HostSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            signal = GameObject.Find("ClientSignal");
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            GameObject.Find("botaoC").GetComponent<Button>().interactable = false;
            GameObject.Find("ENTRAR_SALA").GetComponent<Button>().interactable = true;
        }
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene("MenuInicial");
    }
    
    public void ChangeIpAdress()
    {
        string ipAdress =GameObject.Find("InputField").GetComponent<InputField>().text;
        if(ipAdress != "")
            GameObject.Find("NetworkManager").GetComponent<UNetTransport>().ConnectAddress = ipAdress;
    }
   
}
