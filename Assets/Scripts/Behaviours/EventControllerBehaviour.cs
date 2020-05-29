using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventControllerBehaviour : MonoBehaviour
{  
    private Turnos turno;
    enum Turnos
    {
        DecidirIniciante,
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
    }
   public void OnCick()
    {
      
    }
}
