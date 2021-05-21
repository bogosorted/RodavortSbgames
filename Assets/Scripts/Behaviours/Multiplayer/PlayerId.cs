using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PlayerId : NetworkBehaviour
{
    //PlayerId playerid;
    //NetworkManager ntwrk;
    public bool isplayer2; 
    private GameObject canvas;
     //isowner ver essa variavel pra coincidir com Hasautori
    void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }
    [ServerRpc]
    public void CmdTrocouTurno()
    {
        RpcTrocouTurno();
    }
    [ClientRpc]
    void RpcTrocouTurno()
    {
        canvas.GetComponent<EventControllerBehaviour>().TrocouTurno();
    }
    [ServerRpc]
    public void CmdMudarNomeBotao()
    {
        RpcMudarNomeBotao();
    }
    [ServerRpc]
    public void CmdEfeitoRealizado(bool AutoAlvo,int posicaoCarta,int quantidadeDoEfeito,int efeito)
    {
        RpcEfeitoRealizado(AutoAlvo,posicaoCarta,quantidadeDoEfeito,efeito);
    }
    [ClientRpc]
    void RpcEfeitoRealizado(bool AutoAlvo,int posicaoCarta,int quantidadeDoEfeito,int efeito)
    {
        if(!IsOwner)   
        {
            EffectBase a = Factory.Criar(efeito);
            a.quantidadeDoEfeito = quantidadeDoEfeito;   
            a.RealizarEfeitoEm(AutoAlvo? canvas.transform.GetChild(1).GetComponent<MesaBehaviour>().cartas[posicaoCarta]:canvas.transform.GetChild(2).GetComponent<MesaBehaviour>().cartas[posicaoCarta],null);  
            
        }
    }
    [ClientRpc]
    void RpcMudarNomeBotao()
    {
        canvas.GetComponent<EventControllerBehaviour>().MudarNomeBotao();
    }
    [ServerRpc]
    public void CmdAwake()
    {
        for(int i = 0;i < 3; i++)
         {
            //atualizar o numero total de cartas no final do game
            int r = Random.Range(0,canvas.GetComponent<EventControllerBehaviour>().numeroCartas);
            int r2 = Random.Range(0,canvas.GetComponent<EventControllerBehaviour>().numeroCartas);
            RpcAwake(r,r2);
         }


    }   

     [ClientRpc]
    void RpcAwake(int rn, int rn2)
    {
        var mullig =  GameObject.Find("MulliganBackground");
        mullig.GetComponent<Animator>().SetTrigger("Event");
        Mao player = canvas.GetComponent<Mao>();
        PlayerAdversario player2 =  canvas.GetComponent<PlayerAdversario>();
        if(IsOwner)
            {
            //exibir a carta no mulligan
            mullig.GetComponent<MulliganBehaviour>().CriarCarta(rn);
            player.CriarCartaInicio(rn);
            player2.CriarCarta(rn2);
            }
        else
            {       
            // NetworkIdentity ntwrkid = NetworkClient.connection.identity;
            // playerid = ntwrkid.GetComponent<PlayerId>(); 
            //playerid.
            isplayer2 = true; 
            mullig.GetComponent<MulliganBehaviour>().CriarCarta(rn2);   
            player.CriarCartaInicio(rn2);
            player2.CriarCarta(rn);
            }        
        canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(!IsOwner);  
    }
    [ServerRpc]
    public void CmdAtualizarGold(string valor)
    {
        RpcAtualizarGold(valor);
    }
    [ClientRpc]
    void RpcAtualizarGold(string valor)
    {
        if(IsOwner)
        {
            Mao player =canvas.GetComponent<Mao>();
           player.goldPlayer.text =valor;  
        }
        else
        {
           PlayerAdversario adversario = canvas.GetComponent<PlayerAdversario>();
           adversario.goldInimigo.text = valor;
        }
    }
    [ServerRpc]
    public void CmdAtacarCarta(int posAtacador,int posDefensor)
    {
        RpcAtacarCarta(posAtacador,posDefensor);
    }
    [ClientRpc] void RpcAtacarCarta(int posAtacador,int posDefensor)
    {
        if(!IsOwner)
         canvas.GetComponent<PlayerAdversario>().AtacarCarta(posAtacador,posDefensor);
        
    }
    [ServerRpc]
    public void CmdAtacarPlayer(int posAtacador)
    {
        RpcAtacarPlayer(posAtacador);
    }
    [ClientRpc]
    void RpcAtacarPlayer(int posAtacador)
    {
        if(!IsOwner)
            canvas.GetComponent<PlayerAdversario>().AtacarPlayer(posAtacador);
    }
    [ServerRpc]
    public void CmdColocarCartaBaralho(string a)
    {
        RpcColocarCartaBaralho(a);
    }
    [ClientRpc]
     void RpcColocarCartaBaralho(string a)
    {
        if(!IsOwner)
        canvas.GetComponent<PlayerAdversario>().ColocarCartaBaralho(a);
    }
    [ServerRpc]
    public void CmdStopHost()
    {
        RpcStopHost();
    }
    [ClientRpc]
     void RpcStopHost()
    {
        //  if(IsServer)
        // {
        //     GameObject ntworkManager = GameObject.Find("NetworkManager");
        //     ntwrk = ntworkManager.GetComponent<NetworkNewHud>().manager;
        //     NetworkManager.singleton.StopHost();       
        //     ntwrk.StopHost();
        // }
        // else
        // {
        //     GameObject ntworkManager = GameObject.Find("NetworkManager");
        //     ntwrk = ntworkManager.GetComponent<NetworkNewHud>().manager;          
        //     ntwrk.StopClient();
        // }
    }
    [ServerRpc]
    public void CmdTirarCartaBaralho(int a)
    {
        RpcTirarCartaBaralho(a);
    }
   [ClientRpc]
    void RpcTirarCartaBaralho(int a)
   {
       if(!IsOwner)
        canvas.GetComponent<PlayerAdversario>().TirarCarta(a);
   }
   [ServerRpc]
   public void CmdVoltarCartaBaralho()
   {
     RpcVoltarCartaBaralho();

   }
   [ClientRpc]
    void RpcVoltarCartaBaralho()
   {
       if(!IsOwner)
        canvas.GetComponent<PlayerAdversario>().VoltarBaralho();
   }
    [ServerRpc]
    public void CmdInverterTurnoPlayers() { RpcInverterTurnos(); }
    [ClientRpc]
    void RpcInverterTurnos()
    {
	// if(!IsOwner)
    //     canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(true);
	// if(IsOwner)
	//     canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(false);
    }
    [ServerRpc]
    public void CmdMudarTurno(int turno)
    {
        RpcMudarTurno(turno);
    }
    [ClientRpc]
    void RpcMudarTurno(int turno)
    {
        EventControllerBehaviour.turno = (EventControllerBehaviour.Turnos)turno;
    
    }
    [ServerRpc]
    public void CmdExibirTurno()
    {
        RpcExibirTurno();
        
    }
    [ClientRpc]
    void RpcExibirTurno()
    {
            canvas.GetComponent<EventControllerBehaviour>().ExibirTurno();
            if(!IsOwner)
            canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(true);
            if(IsOwner)
             canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(false);
    }
    // confirmar se é inicio
    [ServerRpc]
    public void CmdCriarCartaInicio(int id)
    {      
        RpcCriarCartaInicio(id);
    }
    [ClientRpc]
    void RpcCriarCartaInicio(int id)
    {
        //funcionando
        if(IsOwner)
        {
            canvas.GetComponent<Mao>().CriarCarta(id);
        }
        else{
            canvas.GetComponent<PlayerAdversario>().CriarCarta(id);
        }
    }  
    [ServerRpc]
    public void CmdInvoke(int id)
    {
             RpcInvoke(id);
    }
    [ClientRpc] void RpcInvoke(int id)
    {
        if(IsOwner)
         canvas.GetComponent<EventControllerBehaviour>().InvokeLater(id);
    }
}