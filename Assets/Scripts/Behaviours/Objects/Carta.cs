using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
public class Carta : MonoBehaviour
{
    private Sprite _cartaImagem;
    private Vector2 _posicaoInicial,_posicaoFinal;
    private Vector3 _angulacaoInicial,_angulacaoFinal;
    private string _nome, _descricao;
    private int _ataque, _defesa;

    #region Propiedades
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

    public Sprite Imagem
    {
        get { return _cartaImagem; }
        set
        {
            _cartaImagem = value;
            gameObject.GetComponent<SpriteRenderer>().sprite = _cartaImagem;
        }
    }

    public string Nome
    {
        get { return _nome; }
        set { _nome = value; }
    }
    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    public int Ataque
    {
        get { return _ataque; }
        set { _ataque = value; }
    }
    public int Defesa
    {
        get { return _defesa; }
        set { _defesa = value; }
    }
    public void Constructor(int id)
    {
        switch (id)
        {
            case -1:
                break;
            case 0:
                Nome = "Trovador";
                Descricao = "Personificação da humanidade artistica";
                Ataque = 6; 
                Defesa = 5;
                goto case -1;      
        }
    }
    #endregion
}


