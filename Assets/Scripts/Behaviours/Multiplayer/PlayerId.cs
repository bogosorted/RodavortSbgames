using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.SceneManagement;  

public class PlayerId : NetworkBehaviour
{
     public bool isPlayer2;
     void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        #region Handlers
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnServerStarted += OnHostConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        #endregion           
        
        if(!IsOwner){return;}

         if(NetworkManager.Singleton.IsConnectedClient)
        {
            isPlayer2 = true;
            print("player2 true setado");
            OnHostConnected();
            OnClientConnected();
            GameObject.Find("botaoC").GetComponent<Button>().interactable = true;
            GameObject.Find("ENTRAR_SALA").GetComponent<Button>().interactable = false;  
            GameObject.Find("CRIAR_SALA").GetComponent<Button>().interactable = false;
            PlayersEstaoProntosServerRpc(); 
          
        }
    }   
 
       void OnClientConnected (ulong clientId = 0) 
    {
         if(!(SceneManager.GetActiveScene().name == "MenuMultiplayer")) return;

        GameObject signal = GameObject.Find("ClientSignal");
        signal.GetComponent<Image>().color = new Color32(59,140,72,255);
    }
      void OnHostConnected () 
    {
        if(!(SceneManager.GetActiveScene().name == "MenuMultiplayer")) return;

        GameObject signal = GameObject.Find("HostSignal");
        signal.GetComponent<Image>().color = new Color32(59,140,72,255);
    }
      void OnClientDisconnect (ulong clientId) 
    {
        if((SceneManager.GetActiveScene().name == "Campo"))
        {
             if(NetworkManager.Singleton.IsHost)
             {
                 NetworkManager.Singleton.StopHost();   
             }
            SceneManager.LoadScene("MenuMultiplayer");
        }
        else if((SceneManager.GetActiveScene().name == "MenuMultiplayer"))
        {
            GameObject signal;
            signal = GameObject.Find("ClientSignal");
            if(!NetworkManager.Singleton.IsHost)
            {
                //caso o client saia
                signal.GetComponent<Image>().color = new Color32(64,64,64,255);
                signal = GameObject.Find("HostSignal");
                GameObject.Find("botaoC").GetComponent<Button>().interactable = false;
                GameObject.Find("ENTRAR_SALA").GetComponent<Button>().interactable = true;
                GameObject.Find("CRIAR_SALA").GetComponent<Button>().interactable = true;
            }
            signal.GetComponent<Image>().color = new Color32(64,64,64,255);
            GameObject.Find("JOGAR").GetComponent<Button>().interactable = false;
        } 
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void ChangeTheSceneServerRpc(string cena)
    {
         ChangeTheSceneClientRpc(cena);
    }
    [ClientRpc]
    public void ChangeTheSceneClientRpc(string cena)
    {
        SceneManager.LoadScene(cena);
    }
    
    [ServerRpc]
    public void PlayersEstaoProntosServerRpc()
    {
        PlayersEstaoProntosClientRpc();
    }
     [ClientRpc]
     void PlayersEstaoProntosClientRpc()
     {
        GameObject.Find("JOGAR").GetComponent<Button>().interactable = true;
     }

//     //PlayerId playerid;
//     //NetworkManager ntwrk;
//     public bool isplayer2; 
//     private GameObject canvas;
//      //isowner ver essa variavel pra coincidir com Hasautori
//     void Awake()
//     {
//         canvas = GameObject.Find("Canvas");
//     }
//     [ServerRpc]
//     public void CmdTrocouTurno()
//     {
//         RpcTrocouTurno();
//     }
//     [ClientRpc]
//     void RpcTrocouTurno()
//     {
//         canvas.GetComponent<EventControllerBehaviour>().TrocouTurno();
//     }
//     [ServerRpc]
//     public void CmdMudarNomeBotao()
//     {
//         RpcMudarNomeBotao();
//     }
//     [ServerRpc]
//     public void CmdEfeitoRealizado(bool AutoAlvo,int posicaoCarta,int quantidadeDoEfeito,int efeito)
//     {
//         RpcEfeitoRealizado(AutoAlvo,posicaoCarta,quantidadeDoEfeito,efeito);
//     }
//     [ClientRpc]
//     void RpcEfeitoRealizado(bool AutoAlvo,int posicaoCarta,int quantidadeDoEfeito,int efeito)
//     {
//         if(!IsOwner)   
//         {
//             EffectBase a = Factory.Criar(efeito);
//             a.quantidadeDoEfeito = quantidadeDoEfeito;   
//             a.RealizarEfeitoEm(AutoAlvo? canvas.transform.GetChild(1).GetComponent<MesaBehaviour>().cartas[posicaoCarta]:canvas.transform.GetChild(2).GetComponent<MesaBehaviour>().cartas[posicaoCarta],null);  
            
//         }
//     }
//     [ClientRpc]
//     void RpcMudarNomeBotao()
//     {
//         canvas.GetComponent<EventControllerBehaviour>().MudarNomeBotao();
//     }
//cmdAwake

//host client ->host server
  //  [ServerRpc]
    public void PedidoMulliganHost()
    {
        if(NetworkManager.Singleton.IsServer)
            for(int i = 0;i < 3; i++)
            {                               //atualizar o numero total de cartas no final do game
                int r = Random.Range(0,GameObject.Find("Canvas").GetComponent<EventControllerBehaviour>().numeroCartas);
                int r2 = Random.Range(0,GameObject.Find("Canvas").GetComponent<EventControllerBehaviour>().numeroCartas);
                ReciboMulliganClientRpc(r,r2);
            }
    }   

//rpcawake
    [ClientRpc]
    void ReciboMulliganClientRpc(int rn, int rn2)
    {
        var mullig =  GameObject.Find("MulliganBackground");
        mullig.GetComponent<Animator>().SetTrigger("Event");
        Mao player = GameObject.Find("Canvas").GetComponent<Mao>();
        PlayerAdversario player2 =  GameObject.Find("Canvas").GetComponent<PlayerAdversario>();
        if(!IsOwner)
            {
            mullig.GetComponent<MulliganBehaviour>().CriarCarta(rn);
            player.CriarCartaInicio(rn);
            player2.CriarCarta(rn2);
            }
        else
            {       
            mullig.GetComponent<MulliganBehaviour>().CriarCarta(rn2);   
            player.CriarCartaInicio(rn2);
            player2.CriarCarta(rn);
            }        
        GameObject.Find("Canvas").GetComponent<EventControllerBehaviour>().BotaoInteragivel(IsOwner);  
    }
//     [ServerRpc]
//     public void CmdAtualizarGold(string valor)
//     {
//         RpcAtualizarGold(valor);
//     }
//     [ClientRpc]
//     void RpcAtualizarGold(string valor)
//     {
//         if(IsOwner)
//         {
//             Mao player =canvas.GetComponent<Mao>();
//            player.goldPlayer.text =valor;  
//         }
//         else
//         {
//            PlayerAdversario adversario = canvas.GetComponent<PlayerAdversario>();
//            adversario.goldInimigo.text = valor;
//         }
//     }
//     [ServerRpc]
//     public void CmdAtacarCarta(int posAtacador,int posDefensor)
//     {
//         RpcAtacarCarta(posAtacador,posDefensor);
//     }
//     [ClientRpc] void RpcAtacarCarta(int posAtacador,int posDefensor)
//     {
//         if(!IsOwner)
//          canvas.GetComponent<PlayerAdversario>().AtacarCarta(posAtacador,posDefensor);
        
//     }
//     [ServerRpc]
//     public void CmdAtacarPlayer(int posAtacador)
//     {
//         RpcAtacarPlayer(posAtacador);
//     }
//     [ClientRpc]
//     void RpcAtacarPlayer(int posAtacador)
//     {
//         if(!IsOwner)
//             canvas.GetComponent<PlayerAdversario>().AtacarPlayer(posAtacador);
//     }
//     [ServerRpc]
//     public void CmdColocarCartaBaralho(string a)
//     {
//         RpcColocarCartaBaralho(a);
//     }
//     [ClientRpc]
//      void RpcColocarCartaBaralho(string a)
//     {
//         if(!IsOwner)
//         canvas.GetComponent<PlayerAdversario>().ColocarCartaBaralho(a);
//     }
//     [ServerRpc]
//     public void CmdStopHost()
//     {
//         RpcStopHost();
//     }
//     [ClientRpc]
//      void RpcStopHost()
//     {
//         //  if(IsServer)
//         // {
//         //     GameObject ntworkManager = GameObject.Find("NetworkManager");
//         //     ntwrk = ntworkManager.GetComponent<NetworkNewHud>().manager;
//         //     NetworkManager.singleton.StopHost();       
//         //     ntwrk.StopHost();
//         // }
//         // else
//         // {
//         //     GameObject ntworkManager = GameObject.Find("NetworkManager");
//         //     ntwrk = ntworkManager.GetComponent<NetworkNewHud>().manager;          
//         //     ntwrk.StopClient();
//         // }
//     }
//TIRARCARTABARALHO
    [ServerRpc]
    public void TirarCartaBaralhoInimigoServerRpc(int PosicaoCartaInimigo)
    {
        AtualizarCartaRetiradaBaralhoInimigoClientRpc(PosicaoCartaInimigo);
    }
   [ClientRpc]
    void AtualizarCartaRetiradaBaralhoInimigoClientRpc(int PosicaoCartaInimigo)
    {
       if(!IsOwner)
            GameObject.Find("Canvas").GetComponent<PlayerAdversario>().TirarCarta(PosicaoCartaInimigo);
    }
//    [ServerRpc]
//    public void CmdVoltarCartaBaralho()
//    {
//      RpcVoltarCartaBaralho();

//    }
//    [ClientRpc]
//     void RpcVoltarCartaBaralho()
//    {
//        if(!IsOwner)
//         canvas.GetComponent<PlayerAdversario>().VoltarBaralho();
//    }
//     [ServerRpc]
//     public void CmdInverterTurnoPlayers() { RpcInverterTurnos(); }
//     [ClientRpc]
//     void RpcInverterTurnos()
//     {
// 	// if(!IsOwner)
//     //     canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(true);
// 	// if(IsOwner)
// 	//     canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(false);
//     }
//     [ServerRpc]
//     public void CmdMudarTurno(int turno)
//     {
//         RpcMudarTurno(turno);
//     }
//     [ClientRpc]
//     void RpcMudarTurno(int turno)
//     {
//         EventControllerBehaviour.turno = (EventControllerBehaviour.Turnos)turno;
    
//     }
//     [ServerRpc]
//     public void CmdExibirTurno()
//     {
//         RpcExibirTurno();
        
//     }
//     [ClientRpc]
//     void RpcExibirTurno()
//     {
//             canvas.GetComponent<EventControllerBehaviour>().ExibirTurno();
//             if(!IsOwner)
//             canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(true);
//             if(IsOwner)
//              canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(false);
//     }
//     // confirmar se é inicio
// criarCaRTAiNICIO
    [ServerRpc]
    public void AdicionarAoBaralhoServerRpc(int id)
    {      
        AdicionarAoBaralhoClientRpc(id);
    }
    [ClientRpc]
    void AdicionarAoBaralhoClientRpc(int id)
    {
        //funcionando
        if(IsOwner)
        {
            GameObject.Find("Canvas").GetComponent<Mao>().CriarCarta(id);
        }
        else{
            GameObject.Find("Canvas").GetComponent<PlayerAdversario>().CriarCarta(id);
        }
    }  
//     [ServerRpc]
//     public void CmdInvoke(int id)
//     {
//              RpcInvoke(id);
//     }
//     [ClientRpc] void RpcInvoke(int id)
//     {
//         if(IsOwner)
//          canvas.GetComponent<EventControllerBehaviour>().InvokeLater(id);
//     }
}