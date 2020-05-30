using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaInimigo : MonoBehaviour
{
    private float _ataque, _defesa;
     private Sprite _cartaImagem;
    public void definirComeco(float ataq,float def,string numero)
    {
        Ataque = ataq; 
        Defesa = def;
        Imagem = Resources.Load<Sprite>("CartasProntas/" + numero); 
    }
   public Sprite Imagem
    {
        get { return _cartaImagem; }
        set
        {
            _cartaImagem = value;
        }
    }
     public float Ataque
    {
        get { return _ataque; }
        set 
        {
            _ataque = value;
        }
    }
    public float Defesa
    {
        get { return _defesa; }
        set
        { 
            _defesa = value; 
        }
    }
    public Vector2 PosicaoInicial
    {
        get;set;        
    }
    public Vector2 PosicaoFinal
    {
        get;set;        
    }
    public Vector3 AngulacaoInicial
    {
        get;set;        
    }

    public Vector3 AngulacaoFinal
    {
        get;set;        
    }
    
    public int PosicaoBaralho
    {
        get;set;
    }
    public void Constructor(int id)
    {
        switch (id)
        {
            case -1:
                break;
            case 0:
                definirComeco(6f,6f,"00");    
             goto case -1; 

            case 1:
                definirComeco(6f,6f,"01");
             goto case -1;

            case 2:
                definirComeco(5f,3f,"02");
            goto case -1;

            case 3:
                definirComeco(5f,3f,"03");
            goto case -1;

            case 4:
                definirComeco(5f,3f,"04");
            goto case -1;

            case 5:
                definirComeco(5f,3f,"05");
            goto case -1;

            case 6:
                definirComeco(5f,3f,"06");
            goto case -1;

            case 7:
                definirComeco(5f,3f,"07");
            goto case -1;

            case 8:
                definirComeco(5f,3f,"08");
            goto case -1;

            case 9:
                definirComeco(5f,3f,"09");
            goto case -1;

            case 10:
                definirComeco(5f,3f,"10");
            goto case -1;
            case 11:
                definirComeco(5f,3f,"11");
            goto case -1;
            case 12:
                definirComeco(5f,3f,"12");
            goto case -1;
        }
    }
}
