using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdversario : MonoBehaviour
{
     [SerializeField] private GameObject carta;
     [Header("Animações do Baralho")]
     [SerializeField] float velocidadeAnimacao = 1f;
     [SerializeField]float distancia = 1;
     [SerializeField] float indiceAngulacao = 12;
     [SerializeField] float altitude = -290 ;
     [SerializeField] float latitude = 0; 
     
    public List<GameObject> maoAdversaria = new List<GameObject>();
    private float vida;
    float x,y;   
    float distanciamentoCartasMaximo;
    bool animarBaralho;
    void Start()
    {
      vida = 30f;
      CriarCarta(Random.Range(0,13));
      CriarCarta(Random.Range(0,13));
      CriarCarta(Random.Range(0,13));
    }
    //posicao atacar é o index do atacador na mesa do baralho inimigo e posicao inimigo 
    //é a posicao da carta do player que sera atacada
    public void AtacarCarta(int posicaoAtacar, int posicaoInimigo)
    {
        MesaBehaviour atacante =  transform.GetChild(2).GetComponent<MesaBehaviour>();
        MesaBehaviour defensor =  transform.GetChild(1).GetComponent<MesaBehaviour>();
        if(atacante.cartas.Count > 0 && defensor.cartas.Count > 0)
        {
           atacante.cartas[posicaoAtacar].transform.GetComponent<Animator>().SetTrigger("Atacar");
           StartCoroutine(DarDano(defensor.cartas[posicaoInimigo],atacante.cartas[posicaoAtacar]));
        }
    }
    public void ColocarCartaBaralho(GameObject cartaColocada)
    {
        CartaInimigo atributos = cartaColocada.GetComponent<CartaInimigo>();
        MesaBehaviour mesa = transform.GetChild(2).GetComponent<MesaBehaviour>();
        maoAdversaria.RemoveAt(atributos.PosicaoBaralho);  
        distanciamentoCartasMaximo -= 10;
        SetAnimacao(distanciamentoCartasMaximo);   
        mesa.CriarCartaInicio(atributos.Ataque,atributos.Defesa,atributos.Imagem);
        cartaColocada.GetComponent<Animator>().SetBool("autoDestruir",true);
    }
    void FixedUpdate()
    {
        if(animarBaralho)
        {
            Angular();
        }
    }
      public void CriarCarta(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.GetComponent<CartaInimigo>().Constructor(id);
        objCarta.transform.SetParent(transform.GetChild(3), false);
        objCarta.transform.localPosition += new Vector3(600, 100);  
        maoAdversaria.Add(objCarta);
        distanciamentoCartasMaximo += 10;
        SetAnimacao(distanciamentoCartasMaximo);
    }

      public void SetAnimacao(float distanciamentoCartasMaximo) 
    {
        // formula que leva em conta um valor de distancia do ponto 0 qualquer (distanciamentoDeCartaMaximo), e a quantidade de vezes
        // em que essa distancia é dividida igualmente (Quantidade de cartas). Devolvendo a constante de distanciamento (Levando em conta 
        // a imparidade ou paridade da divisão.

        //constante de distanciamento
        float angulacaoConst = maoAdversaria.Count % 2 == 0f ? distanciamentoCartasMaximo / (float)(maoAdversaria.Count / 2) : distanciamentoCartasMaximo / (float)((maoAdversaria.Count - 1) / 2);
        //distancia inicial
        float concatenador = -distanciamentoCartasMaximo;
        int index = 0;
        if (maoAdversaria.Count == 1)
            concatenador = 0;
        foreach (var obj in maoAdversaria)
        {
            //setando ID da carta em relação ao baralho
            obj.GetComponent<CartaInimigo>().PosicaoBaralho = index;
            // Setando posição da carta final e inicial 
            obj.GetComponent<CartaInimigo>().PosicaoInicial = obj.transform.localPosition;
            obj.GetComponent<CartaInimigo>().PosicaoFinal = new Vector2(concatenador * distancia + latitude, Mathf.Abs(concatenador) / 5 + altitude);
            // Setando a Angulação final e inicial
            obj.GetComponent<CartaInimigo>().AngulacaoInicial = obj.transform.eulerAngles;
            obj.GetComponent<CartaInimigo>().AngulacaoFinal = new Vector3(0, 0, (concatenador  / indiceAngulacao) );
            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(index);
            index++;
        }
        animarBaralho = true;
        x = 0;
    }
      // executa a animação de movimentação do baralho do ponto inicial ao final ja setado com a suavização
    // dada pela função final ja setado com a suavização dada pela função "f(x)= -x² + 2x" 
    private void Angular() 
    {
        //função
        y = -x*x + 2*x;
        //velocidade da animação
        x += (velocidadeAnimacao * Time.deltaTime);
        //dado o fim da animação
        if (x >= 1)
        {
            animarBaralho = false;
            return;
        }
        //animando todas as cartas da mão
        //por meio do metodo vector.lerp
        foreach(var obj in maoAdversaria)
        {
            CartaInimigo atributos =  obj.GetComponent<CartaInimigo>();
            atributos.AngulacaoInicial = (atributos.AngulacaoInicial.z > 180) ? atributos.AngulacaoInicial - Vector3.forward *360 : atributos.AngulacaoInicial;
            atributos.AngulacaoFinal = (atributos.AngulacaoFinal.z > 180) ? atributos.AngulacaoFinal - Vector3.forward *360 : atributos.AngulacaoFinal;
            obj.transform.localPosition = Vector2.Lerp(atributos.PosicaoInicial,atributos.PosicaoFinal, y);
            obj.transform.eulerAngles = Vector3.Lerp(atributos.AngulacaoInicial , atributos.AngulacaoFinal,y);
        }

    }
    IEnumerator DarDano(GameObject obj, GameObject atacante)
    {
     yield return new WaitForSeconds(0.4f);
     transform.GetComponent<Mao>().Audio(2);
     obj.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa-= atacante.transform.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque;
    }
}
