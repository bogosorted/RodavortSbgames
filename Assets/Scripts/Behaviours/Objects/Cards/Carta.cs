using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Carta : MonoBehaviour
{
    private Sprite _cartaImagem;
    private Vector2 _posicaoInicial,_posicaoFinal;
    private Vector3 _angulacaoInicial,_angulacaoFinal;
    private string _nome, _descricao;
    private float _ataque, _defesa, _valor;
    private int _posicaoBaralho;
    private Evento _ativarPassivaQuando;
    private AlvoPassiva _alvo;
    private PassivaComulativa passiva;
  

    void autoDestruir()
    {   
        Destroy(this.gameObject);
    }
    public void definirComeco(string numero)
    {
        Id = numero;t
        Card refCard = Resources.Load<Card>("InformacoesCartas/" + numero);
        Imagem = Resources.Load<Sprite>("CartasProntas/" + numero);  
         //TESTE
        AtivarPassivaQuando = refCard.ativarPassivaQuando;
        Alvo = refCard.alvoDaPassiva;
         //FIM TESTE
        Nome = refCard.nome;
        Descricao = refCard.desc;
        Ataque = refCard.dano; 
        Defesa =refCard.vida;
        Valor = refCard.valor;
        //// testando //////////
        IsAtivo = AtivarPassivaQuando == Evento.CartaEfeito;
        ////////////////////////    
        passiva = new PassivaComulativa(refCard.quantidadePassiva,refCard.passiva);
        
    }
    #region Propiedades
    public bool IsAtivo{get;set;}
    public string Id{get;set;}
    public Evento AtivarPassivaQuando
    {
        get { return _ativarPassivaQuando; }
        set
        {
            _ativarPassivaQuando = value;
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
      public PassivaComulativa Passiva
    {
        get{return passiva;}
        set{passiva = value;}
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
    public Sprite Imagem
    {
        get { return _cartaImagem; }
        set
        {
            _cartaImagem = value;
             transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = _cartaImagem;

        }
    }

    public string Nome
    {
        get { return _nome; }
        set
         {
             _nome = value;
            transform.GetChild(0).GetChild(4).GetComponent<Text>().text = _nome;
         }
    }
    public string Descricao
    {
        get { return _descricao; }
        set { _descricao = value; }
    }
    public float Ataque
    {
        get { return _ataque; }
        set 
        {
            _ataque = value;
            transform.GetChild(0).GetChild(3).GetComponent<Text>().text = _ataque.ToString();
        }
    }
    public float Defesa
    {
        get { return _defesa; }
        set
        { 
            _defesa = value; 
            transform.GetChild(0).GetChild(2).GetComponent<Text>().text = _defesa.ToString();
        }
    }
    public float Valor
    {
        get{ return _valor;}
        set
        { 
          _valor = value;         
           transform.GetChild(0).GetChild(5).GetComponent<Text>().text = _valor.ToString();
        }
    }
    public void Constructor(int id)
    {
       definirComeco(id < 10 ? "0" + id.ToString() : id.ToString());
      
    }
    #endregion
}


