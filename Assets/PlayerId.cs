using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerId : NetworkBehaviour
{
    PlayerId playerid;
    public bool isplayer2;
    private GameObject canvas;
    void Awake()
    {
        canvas = GameObject.Find("Canvas");
        print(canvas.name);
        print(canvas.transform.GetChild(4).name);
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
    public void RpcAwake(int rn,int rn2)
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
    public void CmdInverterTurnoPlayers() { RpcInverterTurnos(); }
    [ClientRpc]
    void RpcInverterTurnos()
    {
          canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(!hasAuthority);
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
    public void CmdCriarCartaInicio(int id)
    {      
        RpcCriarCartaInicio(id);
    }
    [ClientRpc]
    public void RpcCriarCartaInicio(int id)
    {
        if(hasAuthority)
        {
            print("tem autoridade");
            canvas.GetComponent<Mao>().CriarCarta(id);
        }
        else{
            print("nao tem autoridade");    
            canvas.GetComponent<PlayerAdversario>().CriarCarta(id);
        }
    }  
    [Command]
    public void CmdInvoke(int id)
    {
             RpcInvoke(id);
    }
    [ClientRpc] public void RpcInvoke(int id)
    {
        if(hasAuthority)
         canvas.GetComponent<EventControllerBehaviour>().InvokeLater(id);
    }
}