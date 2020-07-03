using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;


public class EventControllerBehaviour : MonoBehaviour
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
    Efeitos efeitoAtual;
    [SerializeField] private Selectable botao;
    
    public enum Turnos
    {
        DecidirIniciante = 1,
        DecidirCartaInicial,
        TurnoEscolhaP1,
        TurnoEscolhaP2,
        TurnoAtaqueP1,
        TurnoAtaqueP2,
        Vitoria,
        Derrota
    }

    private void Start() {
        turno = Turnos.TurnoEscolhaP1;
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
    public void RealizarPassivaEm(PassivaComulativa efeito, AlvoPassiva alvo,bool player1)
    {
        switch(alvo)
        {
            case AlvoPassiva.CartaAdversaria:
                if(player1)                  
                    foreach(var obj in CartasPlayer.cartas)
                    {    
                        obj.transform.GetChild(0).GetComponent<CartaNaMesa>().AdicionarPassiva(efeito);
                    }
                else
                {
                    foreach(var obj in CartasInimigo.cartas)
                    {          
                        obj.transform.GetChild(0).GetComponent<CartaNaMesa>().AdicionarPassiva(efeito);
                    }
                }
            break;
            case AlvoPassiva.TodasAsCartas:
                foreach(var obj in CartasPlayer.cartas)
                {    
                    obj.transform.GetChild(0).GetComponent<CartaNaMesa>().AdicionarPassiva(efeito);
                }
                foreach(var obj in CartasInimigo.cartas)
                {          
                    obj.transform.GetChild(0).GetComponent<CartaNaMesa>().AdicionarPassiva(efeito);
                }
                break;
        }
    }
    //metodo sobrecarregado para alvos especificos
    public void RealizarPassivaEm(PassivaComulativa efeito, GameObject alvo,bool player1)
    {
        
    }
     public void OnClick()
    {
        if (preparado && (int)turno < System.Enum.GetNames(typeof(Turnos)).Length - 2)
        {
            turno += 1;
        }
        //else só p testar dps tem q tirar isso aq e colocar a derrota ou vitória k
        else
        {
            //quando reseta o turno 
            ouroMaximo = (ouroMaximo < ouroLimite) ? ouroMaximo + 1 : ouroLimite;
            AtualizarOuro(ouroMaximo);
            // cara ou coroa dps
            turno = Turnos.TurnoEscolhaP1;
            //testando pra ver se tem alguma passiva a ser rodada no novo round
            foreach(var obj in CartasPlayer.cartas)
            {
                CartaNaMesa refCard = obj.transform.GetChild(0).GetComponent<CartaNaMesa>();
                if(refCard.AtivarPassivaQuando == Evento.NovoRound)
                {
                    RealizarPassivaEm(refCard.Passiva,refCard.Alvo,true);
                }
                refCard.RodarPassivas();
                
            }
            foreach(var obj in CartasInimigo.cartas)
            {
                CartaNaMesa refCard = obj.transform.GetChild(0).GetComponent<CartaNaMesa>();
                if(refCard.AtivarPassivaQuando == Evento.NovoRound)
                {
                    RealizarPassivaEm(refCard.Passiva,refCard.Alvo,false);
                }
                refCard.RodarPassivas();
            }
            
        }
        preparado = false;
        Invoke(turno.ToString(),0f);
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
    private void DecidirCartaInicial(){
        preparado = true;
    }
    private void TurnoEscolhaP1()
    {
        botao.interactable = true;
        Player.SetRaycast(true);
        Player.CriarCarta(Random.Range(0,13));
        CartasPlayer.SetRaycast(false);
        preparado = true;
    }
    private void TurnoAtaqueP1()
    {
         botao.interactable = true;
         Player.SetRaycast(false);
         CartasPlayer.SetRaycast(true);
         preparado = true;
    }
    private void TurnoEscolhaP2()
    {
        botao.interactable = false;
        //inimigo bot
        StartCoroutine(BotEscolha());
    }
    private void TurnoAtaqueP2(){
       botao.interactable = false;
       AtaqueCartas();
    }
    private void Vitoria(){
        print("Voce Ganhou");
        preparado = true;
    }
    private void Derrota(){
         print("Voce perdeu");
        preparado = true;
    }
  #endregion
    IEnumerator BotEscolha()
    {
        yield return new WaitForSeconds(0f);
        Inimigo.CriarCarta(Random.Range(0,13));   
        GameObject cartaEscolhida = Inimigo.maoAdversaria[Random.Range(0,Inimigo.maoAdversaria.Count -1)];
        if(cartaEscolhida.GetComponent<CartaInimigo>().Valor <= Inimigo.gold)
        {
            Inimigo.ColocarCartaBaralho(cartaEscolhida);
            Inimigo.gold -= cartaEscolhida.GetComponent<CartaInimigo>().Valor;
            Inimigo.goldInimigo.text = string.Format("{0}/{1}",Inimigo.gold, ouroMaximo);
        }
        Player.Audio(1);

        preparado = true;
        OnClick();
    }
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
