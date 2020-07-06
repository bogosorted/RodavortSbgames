using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;


public class CartaNaMesa : MonoBehaviour
{
    private float _ataque, _defesa;
    private Vector2 _posicaoInicial,_posicaoFinal;
    private Sprite _cartaImagem;
    private int _posicaoBaralho;
    private int _quantidadeAtaque;
    private bool _podeAtacar;
    //enum
    private Evento _ativarPassivaQuando;
    //struct
    private PassivaComulativa _passiva;
    //enum
    private AlvoPassiva _alvo;
    //efeitos causados na carta
    private List<PassivaComulativa> efeitosComulativos = new List<PassivaComulativa>();

    void Start()
    {
       print($"{_passiva.efeito} : {_ativarPassivaQuando} : {_alvo}");
    }
    public void AdicionarPassiva(PassivaComulativa a)
    {
         efeitosComulativos.Add(a);
    }
    
    
    public void Destruir()
    {
        MesaBehaviour mesa = transform.parent.transform.parent.GetComponent<MesaBehaviour>();
        mesa.SetAnimacao(mesa.distanciamentoCartasMaximo);
        Destroy(this.transform.parent.gameObject);
    }
    public void definirComeco(float ataq,float def,Sprite img,PassivaComulativa passiva,Evento ativarPassivaQuando,AlvoPassiva alvoDaPassiva)
    {
          
        Imagem = img;  
        AtivarPassivaQuando = ativarPassivaQuando;
        Passiva = passiva;
        Alvo = alvoDaPassiva;
        Defesa = def;
        Ataque = ataq; 
    }
    
    #region Propiedades
    public Evento AtivarPassivaQuando
    {
        get { return _ativarPassivaQuando; }
        set
        {
            _ativarPassivaQuando = value;
        }
    }
    public PassivaComulativa Passiva
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
    public int PosicaoBaralho{get;set;}
     public Vector2 PosicaoInicial{get;set;}
    public Vector2 PosicaoFinal{get;set;}

    public int QuantidadeAtaque{
        get{return _quantidadeAtaque;}
        set
        { 
            _quantidadeAtaque = value ;
           PodeAtacar = value > 0 ? true : false;
        }
    }
    public bool PodeAtacar{
        get{return _podeAtacar;}
        set
        {
            _podeAtacar = value;
            transform.GetChild(3).GetComponent<Image>().enabled = !_podeAtacar;
        }
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
            GameObject Dano = transform.parent.parent.GetComponent<MesaBehaviour>().Dano;
            if(value>_defesa && _defesa != 0)
            {
                Dano = Instantiate(Dano);
                Dano.transform.SetParent(transform.parent.parent, false);
                Dano.transform.localPosition = transform.parent.localPosition + Vector3.up * 50;
                Dano.GetComponent<Text>().text += $"<color=green>+{(value - _defesa).ToString()}</color>";

                _defesa = value; 
            
                transform.GetChild(1).GetComponent<Text>().text = $"<color=green>{_defesa.ToString()}</color>";
            }
            else if(value <_defesa)
            {

                 Dano = Instantiate(Dano);
                 Dano.transform.SetParent(transform.parent.parent, false);
                 Dano.transform.localPosition = transform.parent.localPosition + Vector3.up * 50;
                 Dano.GetComponent<Text>().text += (value - _defesa).ToString();

                 _defesa = value;                 

                if(_defesa <= 0)
                {
                    switch(AtivarPassivaQuando)
                    {
                        case Evento.CartaMorreu:
                      gameObject.transform.parent.transform.parent.transform.parent.GetComponent<EventControllerBehaviour>().RealizarPassivaEm(this.Passiva,this.Alvo,(transform.parent.tag == "Player"),this.gameObject);
                        break;
                    }
                    gameObject.name = "morto";
                    transform.parent.parent.GetComponent<MesaBehaviour>().cartas.RemoveAt(GetComponent<CartaNaMesa>().PosicaoBaralho);
                    transform.parent.parent.GetComponent<MesaBehaviour>().distanciamentoCartasMaximo -= 20;
                    transform.parent.parent.GetComponent<MesaBehaviour>().SetAnimacao(transform.parent.parent.GetComponent<MesaBehaviour>().distanciamentoCartasMaximo);
                    transform.GetComponent<Animator>().SetTrigger("Destruido");
                }
            transform.GetChild(1).GetComponent<Text>().text = $"<color=red>{_defesa.ToString()}</color>";
            }
            else{
                _defesa = value; 
                transform.GetChild(1).GetComponent<Text>().text =_defesa.ToString();
                }
            
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
    #endregion
}
