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

    void autoDestruir()
    {   
        Destroy(this.gameObject);
    }
    void definirComeco(float ataq,float def,float val,string numero)
    {
        Ataque = ataq; 
        Defesa = def;
        Valor = val;
        Imagem = Resources.Load<Sprite>("CartasProntas/" + numero);  
    }
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
        switch (id)
        {
            case -1:
                break;

            case 0:
                Nome = "TROVADOR";
                Descricao = "Personificação da humanidade artistica\n Possui <color=green>" +Defesa+ "</color> de vida ";
                definirComeco(6f,6f,6f,"00");    
             goto case -1; 

            case 1:
                Nome = "BOTICÁRIO";
                Descricao = "ele é bom de cura hein";
                definirComeco(6f,6f,6f,"01");
             goto case -1;

            case 2:
                Nome = "GOBLIN";
                Descricao = "eta esse é brabo";
                definirComeco(5f,3f,4f,"02");
            goto case -1;

            case 3:
                Nome = "CATAPULTA";
                Descricao = "ovo taca pedra";
                definirComeco(5f,3f,4f,"03");
            goto case -1;

            case 4:
                Nome = "MINOTAURO";
                Descricao = "ela é diferente mano";
                definirComeco(5f,3f,4f,"04");
            goto case -1;

            case 5:
                Nome = "CLÍNICO";
                Descricao = "corona";
                definirComeco(5f,3f,4f,"05");
            goto case -1;

            case 6:
                Nome = "ESPADACHIM";
                Descricao = "vo te chifra";
                definirComeco(5f,3f,4f,"06");
            goto case -1;

            case 7:
                Nome = "TRÓPIA";
                Descricao = "";
                definirComeco(5f,3f,4f,"07");
            goto case -1;

            case 8:
                Nome = "HIPER NOVA";
                Descricao = "";
                definirComeco(5f,3f,4f,"08");
            goto case -1;

            case 9:
                Nome = "HIDRA";
                Descricao = "";
                definirComeco(5f,3f,4f,"09");
            goto case -1;

            case 10:
                Nome = "PELEJADOR";
                Descricao = "";
                definirComeco(5f,3f,4f,"10");
            goto case -1;

            case 11:
                Nome = "MINA DE OURO";
                Descricao = "";
                definirComeco(5f,3f,4f,"11");
            goto case -1;
            
            case 12:
                Nome = "TORRE";
                Descricao = "";
                definirComeco(5f,3f,4f,"12");
            goto case -1;
        }
    }
    #endregion
}


