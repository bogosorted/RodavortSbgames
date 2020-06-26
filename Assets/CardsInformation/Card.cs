using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "atributos da carta")]
public class Card : ScriptableObject
{
    public string nome;
    public string desc;
    public float dano;
    public float vida;
    public int valor;
}
