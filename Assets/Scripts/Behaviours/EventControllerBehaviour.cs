using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventControllerBehaviour : MonoBehaviour
{  
    private Turnos turno;
    bool preparado;
    
    private enum Turnos
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
        turno = Turnos.TurnoAtaqueP1;
        preparado = true;
    }
     public void OnCick()
    {
        if(preparado && (int)turno < System.Enum.GetNames(typeof(Turnos)).Length)
        {
          turno = turno + 1;
          Invoke(turno.ToString(),0f);
        }
    }
    private void DecidirIniciante(){
        print("decidirIniciante");
        preparado = true;
    }
    private void DecidirCartaInicial(){}
    private void TurnoEscolhaP1()
    {
        print("TurnoEscolha");
        preparado = true;
    }
    private void TurnoAtaqueP1(){}
    private void TurnoEscolhaP2()
    {
        print("TurnoAtaque");
        preparado = true;
    }
    private void TurnoAtaqueP2(){
        print("TurnoAtaqueP2");
        preparado = true;
    }
    private void Vitoria(){}
    private void Derrota(){
         print("Voce perdeu");
        preparado = true;
    }
}
