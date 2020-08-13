using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Telepathy;

public class PlayerId : NetworkBehaviour
{
    PlayerId playerid;
    NetworkManager ntwrk;
    public bool isplayer2; 
    private GameObject canvas;
    
    void Awake()
    {
        canvas = GameObject.Find("Canvas");
    }
    [Command]
    public void CmdTrocouTurno()
    {
        RpcTrocouTurno();
    }
    [ClientRpc]
    void RpcTrocouTurno()
    {
        canvas.GetComponent<EventControllerBehaviour>().TrocouTurno();
    }
    [Command]
    public void CmdMudarNomeBotao()
    {
        RpcMudarNomeBotao();
    }
    [Command]
    public void CmdEfeitoRealizado(bool AutoAlvo,int posicaoCarta,int quantidadeDoEfeito,int efeito)
    {
        RpcEfeitoRealizado(AutoAlvo,posicaoCarta,quantidadeDoEfeito,efeito);
    }
    [ClientRpc]
    void RpcEfeitoRealizado(bool AutoAlvo,int posicaoCarta,int quantidadeDoEfeito,int efeito)
    {
        if(!hasAuthority)   
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
    [Command]
    public void CmdAwake()
    {
        for(int i = 0;i < 3; i++)
        {
            //atualizar o numero total de cartas no final do game
            int r = Random.Range(0,13);
            int r2 = Random.Range(0,13);
            RpcAwake(r,r2);
        }

    }
     [ClientRpc]
    void RpcAwake(int rn,int rn2)
    {
       Mao player = canvas.GetComponent<Mao>();
       PlayerAdversario player2 =  canvas.GetComponent<PlayerAdversario>();
    if(hasAuthority)
        {
            
            player.CriarCartaInicio(rn);
            player2.CriarCarta(rn2);
        }
    else
        {       
            NetworkIdentity ntwrkid = NetworkClient.connection.identity;
            playerid = ntwrkid.GetComponent<PlayerId>();
            playerid.isplayer2 = true;     
            player.CriarCartaInicio(rn2);
            player2.CriarCarta(rn);
        }        
        //canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(hasAuthority);  
    }
    [Command]
    public void CmdAtualizarGold(string valor)
    {
        RpcAtualizarGold(valor);
    }
    [ClientRpc]
    void RpcAtualizarGold(string valor)
    {
        if(hasAuthority)
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
    [Command]
    public void CmdAtacarCarta(int posAtacador,int posDefensor)
    {
        RpcAtacarCarta(posAtacador,posDefensor);
    }
    [ClientRpc] void RpcAtacarCarta(int posAtacador,int posDefensor)
    {
        if(!hasAuthority)
         canvas.GetComponent<PlayerAdversario>().AtacarCarta(posAtacador,posDefensor);
        
    }
    [Command]
    public void CmdAtacarPlayer(int posAtacador)
    {
        RpcAtacarPlayer(posAtacador);
    }
    [ClientRpc]
    void RpcAtacarPlayer(int posAtacador)
    {
        if(!hasAuthority)
            canvas.GetComponent<PlayerAdversario>().AtacarPlayer(posAtacador);
    }
    [Command]
    public void CmdColocarCartaBaralho(string a)
    {
        RpcColocarCartaBaralho(a);
    }
    [ClientRpc]
     void RpcColocarCartaBaralho(string a)
    {
        if(!hasAuthority)
        canvas.GetComponent<PlayerAdversario>().ColocarCartaBaralho(a);
    }
    [Command]
    public void CmdStopHost()
    {
        RpcStopHost();
    }
    [ClientRpc]
     void RpcStopHost()
    {
         if(isServer)
        {
            GameObject ntworkManager = GameObject.Find("NetworkManager");
            ntwrk = ntworkManager.GetComponent<NetworkNewHud>().manager;
            NetworkManager.singleton.StopHost();       
            ntwrk.StopHost();
        }
        else
        {
            GameObject ntworkManager = GameObject.Find("NetworkManager");
            ntwrk = ntworkManager.GetComponent<NetworkNewHud>().manager;          
            ntwrk.StopClient();
        }
    }
    [Command]
    public void CmdTirarCartaBaralho(int a)
    {
        RpcTirarCartaBaralho(a);
    }
   [ClientRpc]
    void RpcTirarCartaBaralho(int a)
   {
       if(!hasAuthority)
        canvas.GetComponent<PlayerAdversario>().TirarCarta(a);
   }
   [Command]
   public void CmdVoltarCartaBaralho()
   {
     RpcVoltarCartaBaralho();

   }
   [ClientRpc]
    void RpcVoltarCartaBaralho()
   {
       if(!hasAuthority)
        canvas.GetComponent<PlayerAdversario>().VoltarBaralho();
   }
    [Command]
    public void CmdInverterTurnoPlayers() { RpcInverterTurnos(); }
    [ClientRpc]
    void RpcInverterTurnos()
    {
	// if(!hasAuthority)
    //     canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(true);
	// if(hasAuthority)
	//     canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(false);
    }
    [Command]
    public void CmdMudarTurno(int turno)
    {
        RpcMudarTurno(turno);
    }
    [ClientRpc]
    void RpcMudarTurno(int turno)
    {
        EventControllerBehaviour.turno = (EventControllerBehaviour.Turnos)turno;
    
    }
    [Command]
    public void CmdExibirTurno()
    {
        RpcExibirTurno();
        
    }
    [ClientRpc]
    void RpcExibirTurno()
    {
            canvas.GetComponent<EventControllerBehaviour>().ExibirTurno();
            if(!hasAuthority)
            canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(true);
            if(hasAuthority)
             canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(false);
    }
    [Command]
    public void CmdCriarCartaInicio(int id)
    {      
        RpcCriarCartaInicio(id);
    }
    [ClientRpc]
    void RpcCriarCartaInicio(int id)
    {
        //funcionando
        if(hasAuthority)
        {
            canvas.GetComponent<Mao>().CriarCarta(id);
        }
        else{
            canvas.GetComponent<PlayerAdversario>().CriarCarta(id);
        }
    }  
    [Command]
    public void CmdInvoke(int id)
    {
             RpcInvoke(id);
    }
    [ClientRpc] void RpcInvoke(int id)
    {
        if(hasAuthority)
         canvas.GetComponent<EventControllerBehaviour>().InvokeLater(id);
    }
}