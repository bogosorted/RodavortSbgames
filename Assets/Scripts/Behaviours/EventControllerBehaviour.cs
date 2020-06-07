using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventControllerBehaviour : MonoBehaviour
{  
    public static Turnos turno;
    bool preparado;
    PlayerAdversario Inimigo;
    Mao Player;
    MesaBehaviour CartasPlayer;
    MesaBehaviour CartasInimigo;
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
        CartasInimigo = transform.GetChild(2).GetComponent<MesaBehaviour>();
        CartasPlayer = transform.GetChild(1).GetComponent<MesaBehaviour>();
        preparado = true;
    }
     public void OnClick()
    {
        if(preparado && (int)turno < System.Enum.GetNames(typeof(Turnos)).Length)
            turno = turno + 1;
            
          //else só p testar dps tem q tirar isso aq e colocar a derrota ou vitória k
        else
           turno = Turnos.TurnoEscolhaP1;

        preparado = false;
        Invoke(turno.ToString(),0f);
    }
    private void DecidirIniciante(){
        print("decidirIniciante");
        preparado = true;
    }
    private void DecidirCartaInicial(){
        print("decidirCartaInicial");
        preparado = true;
    }
    private void TurnoEscolhaP1()
    {
        botao.interactable = true;
        Player.SetRaycast(true);
        Player.CriarCarta(Random.Range(0,13));
        CartasPlayer.SetRaycast(false);
        print("TurnoEscolhaP1");
        preparado = true;
    }
    private void TurnoAtaqueP1()
    {
         botao.interactable = true;
         print("TurnoAtaqueP1");
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
       StartCoroutine(AtaqueCartas());
    }
    private void Vitoria(){
        print("Voce Ganhou");
        preparado = true;
    }
    private void Derrota(){
         print("Voce perdeu");
        preparado = true;
    }
 
    IEnumerator BotEscolha()
    {
        yield return new WaitForSeconds(0f);
        Inimigo.CriarCarta(Random.Range(0,13));   
        Inimigo.ColocarCartaBaralho(Inimigo.maoAdversaria[Random.Range(0,Inimigo.maoAdversaria.Count -1)]);
        Player.Audio(1);
        print("TurnoEscolhaP2");

        preparado = true;
        OnClick();
    }
    IEnumerator AtaqueCartas()
    {
        //sistema de ataque aleatorio(desconsidera se pode atacar ou n)
        //colocar um sistema melhor depois//metodo Inimigo.AtacarCarta() funcionando perfeitamente.
        if(CartasPlayer.cartas.Count > 0 && CartasInimigo.cartas.Count > 0)
        {
            // o mesmo pode atacar duas vezes em uma só animação nesse esquema aqui
            // só pra testar.
        Inimigo.AtacarCarta(Random.Range(0,CartasInimigo.cartas.Count),Random.Range(0,CartasPlayer.cartas.Count));
        }
        else if(CartasPlayer.cartas.Count == 0)
        {
            Inimigo.AtacarPlayer(Random.Range(0, CartasInimigo.cartas.Count));
        }
        print("TurnoAtaqueP2");
        yield return new WaitForSeconds(0);
        botao.interactable = true;
        preparado = true;    
        OnClick();   
    }
}
