using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaInimigo : MonoBehaviour
{
    // carta na mao do player adversario

    private float _ataque, _defesa;
    private int _valor;
    private Sprite _cartaImagem;
    private Evento _ativarPassivaQuando;
    private AlvoPassiva _alvo;
    private PassivaComulativa passiva; 


    public void autoDestruir()
    {   
        Destroy(this.gameObject);
    }
    public string ID{get;set;}

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

}
