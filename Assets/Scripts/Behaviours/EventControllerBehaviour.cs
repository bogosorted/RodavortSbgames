using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using Mirror;


public class EventControllerBehaviour : NetworkBehaviour
{  
    public static Turnos turno;
    [Header("OuroConfig")]
    public static int ouroMaximo;
    private int ouroLimite;
    bool preparado;
    PlayerAdversario Inimigo;
    Mao Player;
    MesaBehaviour CartasPlayer;
    MesaBehaviour CartasInimigo;
    CartaNaMesa passivCard;
    PlayerId playerid;
    Efeitos efeitoAtual;
    [SerializeField] private Selectable botao;
    
    public enum Turnos
    {
        DecidirIniciante,
        DecidirCartaInicial,
        TurnoEscolhaP1,
        TurnoEscolhaP2,
        TurnoAtaqueP1,
        TurnoAtaqueP2,
        Vitoria,
        Derrota
    }

    private void Start() {
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
                    a.RealizarEfeitoEm(CartasPlayer.cartas,realizador);
                    a.RealizarEfeitoEm(CartasInimigo.cartas,realizador);
                break;
            case AlvoPassiva.CartaAleatoriaAliada:
              if(!player1 && CartasInimigo.cartas.Count > 0 || player1 && CartasPlayer.cartas.Count > 0)
              a.RealizarEfeitoEm(player1?  CartasPlayer.cartas[Random.Range(0,CartasPlayer.cartas.Count)] : CartasInimigo.cartas[Random.Range(0,CartasInimigo.cartas.Count)],realizador);
            break;
            case AlvoPassiva.CartaAleatoriaAdversaria:
              if(player1 && CartasInimigo.cartas.Count > 0 || !player1 && CartasPlayer.cartas.Count > 0)
              a.RealizarEfeitoEm(player1?  CartasInimigo.cartas[Random.Range(0,CartasInimigo.cartas.Count)]:CartasPlayer.cartas[Random.Range(0,CartasPlayer.cartas.Count)],realizador);
            break;
            case AlvoPassiva.Carta:
            a.RealizarEfeitoEm(realizador,realizador);
            break;
            
        }
    }

    public void BotaoInteragivel(bool valor)
    {
        botao.interactable = valor;
    }
    //metodo sobrecarregado para alvos especificos
     public void OnClick()
    {
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if (preparado && (int)turno < System.Enum.GetNames(typeof(Turnos)).Length - 2)
        {
            print(turno);
            turno += 1;
            print(turno);
            playerid.CmdMudarTurno((int)turno);
        }
        //else só p testar dps tem q tirar isso aq e colocar a derrota ou vitória k
        else if (preparado)
        {
            playerid.CmdTrocouTurno();         
        }
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
    }
    private void DecidirCartaInicial(){
        GameObject[] numerosPlayers = GameObject.FindGameObjectsWithTag("PlayerId");
        if(numerosPlayers.Length == 2)// true 
        {
            Player.SetRaycast(true);
            NetworkIdentity ntwrkid = NetworkClient.connection.identity;
            playerid = ntwrkid.GetComponent<PlayerId>();
            playerid.CmdAwake();
            preparado = true;
            turno = Turnos.TurnoEscolhaP1;
            playerid.CmdMudarTurno((int)turno);
            playerid.CmdInverterTurnoPlayers();
        }
            else
            preparado =  false;
            //mudar para falso no final dos testes
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
        playerid.CmdInverterTurnoPlayers();
        }
        else
        {
            preparado = false;
        }
    }
    private void TurnoAtaqueP1()
    {
        print("ope");
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if(!playerid.isplayer2)
            {
                print("entrou");
            Player.SetRaycast(true);
            playerid.CmdCriarCartaInicio(Random.Range(0,13));
            playerid.CmdInverterTurnoPlayers();
            CartasPlayer.SetRaycast(true);
            preparado = true;            
            }
            else
        {
            print("nao entrou");
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
        playerid.CmdInverterTurnoPlayers();
        Player.SetRaycast(true);
        playerid.CmdMudarTurno((int)turno);      
        //ATUALIZAR O RANGE MAXIMO DE CARTAS NA VERSÃO FINAL       
        }
    }
        //inimigo bot
        //StartCoroutine(BotEscolha());
    private void TurnoAtaqueP2(){

            NetworkIdentity ntwrkid = NetworkClient.connection.identity;
            playerid = ntwrkid.GetComponent<PlayerId>();
        if(playerid.isplayer2)
            {
                //player1 em game
            print("teoricamente player ");
            Player.SetRaycast(true);
            CartasPlayer.SetRaycast(true);
            preparado = true;
            playerid.CmdCriarCartaInicio(Random.Range(0,13));
            playerid.CmdInverterTurnoPlayers();
            }
             else
            {
                playerid.CmdInverterTurnoPlayers();
                playerid.CmdCriarCartaInicio(Random.Range(0,13));
                Player.SetRaycast(true);
            }
    }
       //bot ataque
       //AtaqueCartas();
    private void Vitoria(){
        NetworkIdentity ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();
        if(!playerid.isplayer2)
        {
        turno = Turnos.TurnoEscolhaP1;
        playerid.CmdInverterTurnoPlayers();
        playerid.CmdMudarTurno((int)turno); 
        OnClick();
        playerid.CmdTrocouTurno();  
        preparado = true;
        }
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
            playerid.CmdInverterTurnoPlayers();
            //testando pra ver se tem alguma passiva a ser rodada no novo round
            foreach(var obj in CartasPlayer.cartas)
            {
                CartaNaMesa refCard = obj.transform.GetChild(0).GetComponent<CartaNaMesa>();
                refCard.QuantidadeAtaque =1;
                if(refCard.AtivarPassivaQuando == Evento.NovoRound)
                    RealizarPassivaEm(refCard.Passiva,refCard.Alvo,true,obj);
                
            }
            foreach(var obj in CartasInimigo.cartas)
            {
                CartaNaMesa refCard = obj.transform.GetChild(0).GetComponent<CartaNaMesa>();
                refCard.QuantidadeAtaque = 1 ;
                if(refCard.AtivarPassivaQuando == Evento.NovoRound)
                    RealizarPassivaEm(refCard.Passiva,refCard.Alvo,false,obj);
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
