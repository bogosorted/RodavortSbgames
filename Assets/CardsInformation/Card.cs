using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "atributos da carta")]
public class Card : ScriptableObject
{
    //testando
    public enum Efeitos
    {
        //atacar é um efeito comum a todas ascartas
        Atacar,
        Curar,
        Sangrear,
        Executar,

    }
    public Efeitos Passiva;
    // fim do teste
    public string nome;
    public string desc;
    public float dano;
    public float vida;
    public int valor;
}
