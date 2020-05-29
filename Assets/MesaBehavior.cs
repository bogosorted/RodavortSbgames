 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;

 public class MesaBehavior : MonoBehaviour
 {
    float x,y;
    [Header("Animações do Baralho")]
    public float velocidadeAnimacao = 1f;
    public float altitude = 0 ;
    public float latitude = 0; 
    [Header("Configurações padrões")]
    float distanciamentoCartasMaximo;
    [SerializeField] private GameObject carta;
    public List<GameObject> cartas = new List<GameObject>();
    bool angularBaralho;
    void Start()
    {
        
    }

    void Update()
    {
        if(angularBaralho)
            Angular();
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
            obj.GetComponent<CartaNaMesa>().PosicaoBaralho = index;
            // Setando posição da carta final e inicial 
            obj.GetComponent<CartaNaMesa>().PosicaoInicial = obj.transform.localPosition;
            obj.GetComponent<CartaNaMesa>().PosicaoFinal = new Vector2(concatenador * latitude,altitude);
            concatenador += angulacaoConst;
            index++;
        }
        angularBaralho = true;
        x = 0;
    }
     public void CriarCartaInicio(float ataq,float def,Sprite img)
     {
         GameObject objCarta = Instantiate(carta);
         objCarta.transform.localPosition = new Vector2(0,-20);
         objCarta.GetComponent<Image>().raycastTarget = false;
         objCarta.GetComponent<CartaNaMesa>().definirComeco(ataq,def,img);
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
            obj.transform.localPosition = Vector2.Lerp(obj.GetComponent<CartaNaMesa>().PosicaoInicial, obj.GetComponent<CartaNaMesa>().PosicaoFinal, y);
        }
    }
 }
