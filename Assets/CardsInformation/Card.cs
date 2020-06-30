﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Efeitos
    {
        //atacar é um efeito comum a todas ascartas
        Nenhum,
        Atacar,
        Curar,
        Sangrear,
        Executar,
        

    }
    public enum Evento
    {
        Nunca,
        CartaIniciada,
        NovoRound,
        CartaAtaque,
        CartaMorreu,
    }
    public enum AlvoPassiva
    {
        Nenhum,
        TodasAsCartas,
        CartasAliado,
        CartasInimigo,
        CartaAtacada,
    }

[CreateAssetMenu(menuName = "atributos da carta")]
public class Card : ScriptableObject
{
    //testando
    
    public Efeitos passiva;
    public Evento ativarPassivaQuando;
    public AlvoPassiva alvoDaPassiva; // por enquanto nem é passada
    // fim do teste
    public string nome;
    public string desc;
    public float dano;
    public float vida;
    public int valor;
}
