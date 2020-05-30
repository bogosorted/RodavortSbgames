using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventControllerBehaviour : MonoBehaviour
{  
    public static Turnos turno;
    bool preparado;
    
    public enum Turnos
    {
        DecidirIniciante = 1,
        DecidirCartaInicial,
        TurnoEscolhaP1,
        TurnoAtaqueP1,
        TurnoEscolhaP2,
        TurnoAtaqueP2,
        Vitoria,
        Derrota
    }
    private void Start() {
        turno = Turnos.TurnoEscolhaP1;
        //preparado diz se ele ja pode ir ou não ao proximo estado
        preparado = true;
    }
     public void OnCick()
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
        GetComponent<Mao>().SetRaycast(true);
        transform.GetChild(1).GetComponent<MesaBehaviour>().SetRaycast(false);
        print("TurnoEscolhaP1");
        preparado = true;
    }
    private void TurnoAtaqueP1()
    {
         print("TurnoAtaqueP1");
         GetComponent<Mao>().SetRaycast(false);
         transform.GetChild(1).GetComponent<MesaBehaviour>().SetRaycast(true);
         preparado = true;
    }
    private void TurnoEscolhaP2()
    {
        print("TurnoEscolhaP2");
        preparado = true;
    }
    private void TurnoAtaqueP2(){
        print("TurnoAtaqueP2");
        preparado = true;
    }
    private void Vitoria(){
        print("Voce Ganhou");
        preparado = true;
    }
    private void Derrota(){
         print("Voce perdeu");
        preparado = true;
    }
}
