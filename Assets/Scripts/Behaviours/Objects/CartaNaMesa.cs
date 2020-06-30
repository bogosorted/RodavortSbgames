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
    private Evento _ativarPassivaQuando;
    private Efeitos _passiva;
    private AlvoPassiva _alvo;


    void Start()
    {
       print($"{_passiva} : {_ativarPassivaQuando} : {_alvo}");
    }
    public void Destruir()
    {
        MesaBehaviour mesa = transform.parent.transform.parent.GetComponent<MesaBehaviour>();
        mesa.SetAnimacao(mesa.distanciamentoCartasMaximo);
        Destroy(this.transform.parent.gameObject);
    }
    public void definirComeco(float ataq,float def,Sprite img,Efeitos passiva,Evento ativarPassivaQuando,AlvoPassiva alvoDaPassiva)
    {
        Ataque = ataq; 
        Defesa = def;
        Imagem = img;  
        AtivarPassivaQuando = ativarPassivaQuando;
        Passiva = passiva;
        Alvo = alvoDaPassiva;
    }
    public Evento AtivarPassivaQuando
    {
        get { return _ativarPassivaQuando; }
        set
        {
            _ativarPassivaQuando = value;
        }
    }
    public Efeitos Passiva
    {
        get { return _passiva; }
        set
        {
            _passiva = value;
        }
    }
     public AlvoPassiva Alvo
    {
        get { return _alvo; }
        set
        {
            _alvo = value;
        }
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
    public float Defesa1 { get; set; }
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
