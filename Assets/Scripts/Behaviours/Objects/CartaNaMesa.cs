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
        Ataque = ataq; 
        Defesa = def;
        Imagem = img;  
        AtivarPassivaQuando = ativarPassivaQuando;
        Passiva = passiva;
        Alvo = alvoDaPassiva;
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
            print(value);
            print(_defesa);
            if(value>_defesa && _defesa != 0)
            {
            _defesa = value; 
            transform.GetChild(1).GetComponent<Text>().text = $"<color=green>{_defesa.ToString()}</color>";
            }
            else if(value <_defesa)
            {
                _defesa = value; 
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
