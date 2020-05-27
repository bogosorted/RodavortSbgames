using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaBehavior : MonoBehaviour
{
    float x,y;
    float velocidadeAnimacao,distanciamentoCartasMaximo,latitude,altitude;
    GameObject carta;
    public List<GameObject> cartas = new List<GameObject>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetAnimacao(float distanciamentoCartasMaximo) 
    {
        // formula que leva em conta um valor de distancia do ponto 0 qualquer (distanciamentoDeCartaMaximo), e a quantidade de vezes
        // em que essa distancia é dividida igualmente (Quantidade de cartas). Devolvendo a constante de distanciamento (Levando em conta 
        // a imparidade ou paridade da divisão.

        //constante de distanciamento
        float angulacaoConst = cartas.Count % 2 == 0f ? distanciamentoCartasMaximo / (float)(cartas.Count / 2) : distanciamentoCartasMaximo / (float)((cartas.Count - 1) / 2);
        //distancia inicial
        float concatenador = -distanciamentoCartasMaximo;
        int index = 0;
        if (cartas.Count == 1)
            concatenador = 0;
        foreach (var obj in cartas)
        {
            //setando ID da carta em relação ao baralho
            obj.GetComponent<Carta>().PosicaoBaralho = index;
            // Setando posição da carta final e inicial 
            obj.GetComponent<Carta>().PosicaoInicial = obj.transform.localPosition;
            obj.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador * latitude, -Mathf.Abs(concatenador) / 5 + altitude);

            if (concatenador == 0 || concatenador == distanciamentoCartasMaximo || concatenador == -distanciamentoCartasMaximo)
                obj.GetComponent<Carta>().PosicaoFinal = new Vector3(concatenador *  latitude, -Mathf.Abs(concatenador) /5 + altitude);
            concatenador += angulacaoConst;
            index++;
        }
        x = 0;
    }
    public void CriarCartaInicio(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.GetComponent<Carta>().Constructor(id);
        objCarta.transform.SetParent(transform, false);
        cartas.Add(objCarta);
        distanciamentoCartasMaximo += 20;
        SetAnimacao(distanciamentoCartasMaximo);
        
    }  
    private void Angular() 
    {
        //função
        y = -x*x + 2*x;
        //velocidade da animação
        x += (velocidadeAnimacao * Time.deltaTime);
        //dado o fim da animação
        if (x >= 1)
        {
            return;
        }
        //animando todas as cartas da mão
        //por meio do metodo vector.lerp
        foreach(var obj in cartas)
        {
            obj.transform.localPosition = Vector2.Lerp(obj.GetComponent<Carta>().PosicaoInicial, obj.GetComponent<Carta>().PosicaoFinal, y);
        }
    }
}
