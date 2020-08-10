using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using Mirror;


public class EventControllerBehaviour : NetworkBehaviour
{  
    // tirar essa variavel na versão final do game
    bool testandoNoEditor;

    public static Turnos turno;
    public static int ouroMaximo;
    public GameObject exibicaoTurno;
    [Header("OuroConfig")]
    private int ouroLimite;
    bool preparado;
    bool rodandoPassivasCampoProprio;
    PlayerAdversario Inimigo;
    Mao Player;
    MesaBehaviour CartasPlayer;
    MesaBehaviour CartasInimigo;
    CartaNaMesa passivCard;
    PlayerId playerid;
    Efeitos efeitoAtual;
    public Selectable botao;
    
    public enum Turnos
    {
        DecidirIniciante,
        Inicio,
        TurnoEscolhaP1,
        TurnoEscolhaP2,
        TurnoAtaqueP1,
        TurnoAtaqueP2,
        NovoTurno,
        Derrota
    }

    private void Start() {
        testandoNoEditor = false;

        botao.transform.GetChild(0).GetComponent<Text>().text = "INICIAR";
        turno = Turnos.DecidirIniciante;
        Inimigo = GetComponent<PlayerAdversario>();
        Player =  GetComponent<Mao>();
        ouroLimite = 10;
        ouroMaximo = 1;
        Player.SetarGold(ouroMaximo);
        Inimigo.SetarGold(ouroMaximo);
        CartasInimigo = transform.GetChild(2).GetComponent<MesaBehaviour>();
        CartasPlayer = transform.GetChild(1).GetComponent<MesaBehaviour>();
        preparado = true;
    }
    public void RealizarPassivaEm(PassivaComulativa efeito, AlvoPassiva alvo,bool player1,GameObject realizador)
    {
        EffectBase a = Factory.Criar((int)(efeito.efeito));
        a.quantidadeDoEfeito = efeito.quantidade;
        switch(alvo)
        {
            case AlvoPassiva.CartaAdversaria:
                a.RealizarEfeitoEm(player1?CartasInimigo.cartas:CartasPlayer.cartas,realizador);
            break;

            case AlvoPassiva.CartaAliada:
                 a.RealizarEfeitoEm(player1?  CartasPlayer.cartas: CartasInimigo.cartas,realizador);
                 break;
            case AlvoPassiva.TodasAsCartas:
            //garantir que nos dois pc rodem as passivas na mesma ordem
                    if(!playerid.isplayer2)
                    {
                        a.RealizarEfeitoEm(CartasPlayer.cartas,realizador);
                        a.RealizarEfeitoEm(CartasInimigo.cartas,realizador);
                    }
                    else
                    {
                        a.RealizarEfeitoEm(CartasInimigo.cartas,realizador);
                        a.RealizarEfeitoEm(CartasPlayer.cartas,realizador);
                    }
                break;
            case AlvoPassiva.CartaAleatoriaAliada:
              if(/*!player1 && CartasInimigo.cartas.Count > 0 ||*/CartasPlayer.cartas.Count > 0)
              {
                  if(rodandoPassivasCampoProprio)
                  {
                    var rnd = Random.Range(0,CartasPlayer.cartas.Count);
                    a.RealizarEfeitoEm(CartasPlayer.cartas[rnd],realizador);
                    playerid.CmdEfeitoRealizado(false,rnd,efeito.quantidade,(int)(efeito.efeito));
                  }
              }
              
            break;
            case AlvoPassiva.CartaAleatoriaAdversaria:
              if(CartasInimigo.cartas.Count > 0 /*|| !player1 && CartasPlayer.cartas.Count > 0*/)
              {
                  if(rodandoPassivasCampoProprio)
                  {
                    var rnd = Random.Range(0,CartasInimigo.cartas.Count);
                    a.RealizarEfeitoEm(CartasInimigo.cartas[rnd],realizador);
                    playerid.CmdEfeitoRealizado(true,rnd,efeito.quantidade,(int)(efeito.efeito));
                  }
              }
            break;
            case AlvoPassiva.Carta:
            a.RealizarEfeitoEm(realizador,realizador);
            break;
            
        }
    }
     public void RealizarPassivaEm(PassivaComulativa efeito, GameObject alvo,bool player1,GameObject realizador)
    {
        EffectBase a = Factory.Criar((int)(efeito.efeito));
        a.quantidadeDoEfeito = efeito.quantidade;
        a.RealizarEfeitoEm(alvo,realizador);
    }
    public void MudarNomeBotao()
    {
        botao.transform.GetChild(0).GetComponent<Text>().text = "PRÓXIMO TURNO";
    }

    public void ExibirTurno()
    {
        GameObject exibirTurnoAtual = Instantiate(exibicaoTurno);
        exibirTurnoAtual.transform.SetParent(this.transform,false);
        exibirTurnoAtual.transform.GetChild(0).GetComponent<Text>().text = turno.ToString();

    }
    public void BotaoInteragivel(bool valor)
    {
        botao.interactable = valor;
    }
    //metodo sobrecarregado para alvos especificos
     public void OnClick()
    {
        GameObject[] numerosPlayers = GameObject.FindGameObjectsWithTag("PlayerId");
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if(!testandoNoEditor && numerosPlayers.Length == 2)
            botao.interactable = false;
        if (preparado && (int)turno < System.Enum.GetNames(typeof(Turnos)).Length - 2 && numerosPlayers.Length == 2 || testandoNoEditor)
        {
 
            turno = turno + 1;
            playerid.CmdMudarTurno((int)turno);
            if(!testandoNoEditor)
                playerid.CmdInverterTurnoPlayers();
            playerid.CmdExibirTurno();
        }
        //else só p testar dps tem q tirar isso aq e colocar a derrota ou vitória k
        else if (preparado && numerosPlayers.Length == 2 || testandoNoEditor)
            playerid.CmdTrocouTurno();         
        preparado = false;
        playerid.CmdInvoke((int)turno);
    }
   public void InvokeLater(int id)
   {
        Invoke(((EventControllerBehaviour.Turnos)id).ToString(),0f);
   }
    #region TURNOS
    private void AtualizarOuro(int ouroMaximo) 
    {
        Player.SetarGold(ouroMaximo);
        Inimigo.SetarGold(ouroMaximo);
    }
    private void DecidirIniciante(){
        preparado = true;
    }
    private void Inicio(){
        GameObject[] numerosPlayers = GameObject.FindGameObjectsWithTag("PlayerId");
        if(numerosPlayers.Length == 2 || testandoNoEditor)// true 
        {
            Player.SetRaycast(true);
            NetworkIdentity ntwrkid = NetworkClient.connection.identity;
            playerid = ntwrkid.GetComponent<PlayerId>();
            playerid.CmdAwake();
            preparado = true;
            turno = Turnos.TurnoEscolhaP1;
            playerid.CmdMudarTurno((int)turno);
            if(!testandoNoEditor)
                playerid.CmdInverterTurnoPlayers();
            playerid.CmdMudarNomeBotao();
        }
        else{
            turno = Turnos.DecidirIniciante; 
        }

 
    }
    private void TurnoEscolhaP1()
    {
        
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if(!playerid.isplayer2)
        {
        Player.SetRaycast(true);
        CartasPlayer.SetRaycast(true);
        preparado = true;       
        if(!testandoNoEditor)
            playerid.CmdInverterTurnoPlayers();
        }
        else
        {
            preparado = false;
        }
    }
    private void TurnoAtaqueP1()
    {
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if(!playerid.isplayer2)
            {
            Player.SetRaycast(true);
            playerid.CmdCriarCartaInicio(Random.Range(0,13));
             if(!testandoNoEditor)
                 playerid.CmdInverterTurnoPlayers();
            CartasPlayer.SetRaycast(true);
            preparado = true;            
            }
            else
        {
            preparado= false;
        }
    }

    private void TurnoEscolhaP2()
    {
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if(!playerid.isplayer2)
        {   
            
        }
         else
        {
        preparado = true;
        if(!testandoNoEditor)
            playerid.CmdInverterTurnoPlayers();
        Player.SetRaycast(true);   
        }
    }
    private void TurnoAtaqueP2(){

            NetworkIdentity ntwrkid = NetworkClient.connection.identity;
            playerid = ntwrkid.GetComponent<PlayerId>();
        if(playerid.isplayer2)
            {
                //player1 em game
            Player.SetRaycast(true);
            CartasPlayer.SetRaycast(true);
            preparado = true;
            playerid.CmdCriarCartaInicio(Random.Range(0,13));
             if(!testandoNoEditor)
                playerid.CmdInverterTurnoPlayers();
            }
             else
            {
                // if(!testandoNoEditor)
                //     playerid.CmdInverterTurnoPlayers();
                // playerid.CmdCriarCartaInicio(Random.Range(0,13));
                // Player.SetRaycast(true);
            }
    }
       //bot ataque
       //AtaqueCartas();
    private void NovoTurno(){
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        turno = Turnos.TurnoEscolhaP1;
       // playerid.CmdMudarTurno((int)turno); 
        OnClick();
        if(!testandoNoEditor)
            playerid.CmdInverterTurnoPlayers();
        playerid.CmdTrocouTurno();  
        preparado = true;
    }
    private void Derrota(){
         print("Voce perdeu");
        preparado = true;
    }
   public void TrocouTurno()
    {
//quando reseta o turno 
            ouroMaximo = (ouroMaximo < ouroLimite) ? ouroMaximo + 1 : ouroLimite;
            AtualizarOuro(ouroMaximo);
            // cara ou coroa dps
            turno = Turnos.TurnoEscolhaP1;
            //playerid = ntwrkid.GetComponent<PlayerId>();
            playerid.CmdMudarTurno((int)turno); 
            // o player 2 tem vantagem por rodar a passiva primeiro q o player 1
            foreach(var obj in playerid.isplayer2 ?  CartasInimigo.cartas : CartasPlayer.cartas)
            {
                rodandoPassivasCampoProprio = !playerid.isplayer2;
                CartaNaMesa refCard = obj.transform.GetChild(0).GetComponent<CartaNaMesa>();
                refCard.QuantidadeAtaque =1;
                if(refCard.AtivarPassivaQuando == Evento.NovoRound)
                    RealizarPassivaEm(refCard.Passiva,refCard.Alvo,!(playerid.isplayer2),obj);
                
            }
            foreach(var obj in playerid.isplayer2 ?  CartasPlayer.cartas : CartasInimigo.cartas)
            {
                rodandoPassivasCampoProprio = playerid.isplayer2;
                CartaNaMesa refCard = obj.transform.GetChild(0).GetComponent<CartaNaMesa>();
                refCard.QuantidadeAtaque = 1 ;
                if(refCard.AtivarPassivaQuando == Evento.NovoRound)
                    RealizarPassivaEm(refCard.Passiva,refCard.Alvo,!(playerid.isplayer2),obj);
            }
    }
  #endregion

    void AtaqueCartas()
    {
        //sistema de ataque aleatorio(desconsidera se pode atacar ou n)
        //colocar um sistema melhor depois//metodo Inimigo.AtacarCarta() funcionando perfeitamente.
        if(CartasPlayer.cartas.Count > 0 && CartasInimigo.cartas.Count > 0)
        {
            // o mesmo pode atacar duas vezes em uma só animação nesse esquema aqui
            // só pra testar.
        Inimigo.AtacarCarta(Random.Range(0,CartasInimigo.cartas.Count),Random.Range(0,CartasPlayer.cartas.Count));
        }
        else if(CartasPlayer.cartas.Count == 0 && CartasInimigo.cartas.Count > 0)
        {
            Inimigo.AtacarPlayer(Random.Range(0, CartasInimigo.cartas.Count));
        }
        botao.interactable = true;
        preparado = true;    
        OnClick();   
    }
}