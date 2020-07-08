using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerId : NetworkBehaviour
{
    private GameObject canvas;
    void Awake()
    {
        canvas = GameObject.Find("Canvas");
        print(canvas.name);
        print(canvas.transform.GetChild(4).name);
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
            Mao.isplayer2 = true;     
            player.CriarCartaInicio(rn2);
            player2.CriarCarta(rn);
        }
            
        canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(hasAuthority);
        
    
    }
    [Command]
    public void CmdInverterTurnoPlayers() => RpcInverterTurnos();
    [ClientRpc]
    void RpcInverterTurnos()
    {
          canvas.GetComponent<EventControllerBehaviour>().BotaoInteragivel(!hasAuthority);
          canvas.GetComponent<EventControllerBehaviour>().OnClick();
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
}