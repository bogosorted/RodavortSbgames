using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartaNaMesa : MonoBehaviour
{
    private float _ataque, _defesa;
    private Vector2 _posicaoInicial,_posicaoFinal;
    private Sprite _cartaImagem;
    private int _posicaoBaralho;


    public void Destruir()
    {
        MesaBehaviour mesa = transform.parent.transform.parent.GetComponent<MesaBehaviour>();
        mesa.SetAnimacao(mesa.distanciamentoCartasMaximo);
        Destroy(this.transform.parent.gameObject);
    }
    public void definirComeco(float ataq,float def,Sprite img)
    {
        Ataque = ataq; 
        Defesa = def;
        Imagem = img;  
    }
    public int PosicaoBaralho
    {
        get;set;
    }
     public Vector2 PosicaoInicial
    {
        get;set;        
    }
    public Vector2 PosicaoFinal
    {
        get;set;        
    }
      public float Ataque
    {
        get { return _ataque; }
        set 
        {
            _ataque = value;
            transform.GetChild(2).GetComponent<Text>().text = _ataque.ToString();
        }
    }
      public float Defesa
    {
        get { return _defesa; }
        set
        { 
            _defesa = value; 
            transform.GetChild(1).GetComponent<Text>().text = _defesa.ToString();
        }
    }
     public Sprite Imagem
    {
        get { return _cartaImagem; }
        set
        {
            _cartaImagem = value;
             transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _cartaImagem;
        }
    }
}
