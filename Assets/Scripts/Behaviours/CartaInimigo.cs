using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaInimigo : MonoBehaviour
{
    // carta na mao do player adversario

    private float _ataque, _defesa;
    private int _valor;
    private Sprite _cartaImagem;

    public void definirComeco(string numero)
    {
        Ataque = Resources.Load<Card>("InformacoesCartas/" + numero).dano; 
        Defesa = Resources.Load<Card>("InformacoesCartas/" + numero).vida;
        Valor = Resources.Load<Card>("InformacoesCartas/" + numero).valor;
        Imagem = Resources.Load<Sprite>("CartasProntas/" + numero); 
    }
    public void autoDestruir()
    {   
        Destroy(this.gameObject);
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
    public int Valor
    {
        get { return _valor; }
        set
        { 
            _valor = value; 
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
        definirComeco(id < 10 ? "0" + id.ToString() : id.ToString());
    }
}
