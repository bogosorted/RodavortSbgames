using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAdversario : MonoBehaviour
{
     [SerializeField] public GameObject carta,dano;
     [Header("Animações do Baralho")]
     [SerializeField] float velocidadeAnimacao = 1f;
     [SerializeField]float distancia = 1;
     [SerializeField] float indiceAngulacao = 12;
     [SerializeField] float altitude = -290 ;
     [SerializeField] float latitude = 0;
     [Header("Objetos")]
     [SerializeField] public Text vidaInimigo;
     [SerializeField] public Text goldInimigo;
     
    public List<GameObject> maoAdversaria = new List<GameObject>();
    public GameObject CartaSegurada;
    GameObject Dano;
    float x,y;
    [Header("Vida")]
    public float vida;
    public int gold;
    float distanciamentoCartasMaximo;
    bool animarBaralho;
    void Start()
    {
      vidaInimigo.text = vida + "/" + vida;
    //   CriarCarta(Random.Range(0,13));
    //   CriarCarta(Random.Range(0,13));
    //   CriarCarta(Random.Range(0,13));
    }

    //posicao atacar é o index do atacador na mesa do baralho inimigo e posicao inimigo 
    //é a posicao da carta do player que sera atacada
    public void AtacarCarta(int posicaoAtacar, int posicaoInimigo)
    {
        MesaBehaviour atacante =  transform.GetChild(2).GetComponent<MesaBehaviour>();
        MesaBehaviour defensor =  transform.GetChild(1).GetComponent<MesaBehaviour>();
        //if redundante
        if(atacante.cartas.Count > 0 && defensor.cartas.Count > 0)
        {
           CartaNaMesa refCard = atacante.cartas[posicaoAtacar].transform.GetChild(0).GetComponent<CartaNaMesa>();
           refCard.QuantidadeAtaque--;
            switch(refCard.AtivarPassivaQuando)
                {             
                case Evento.CartaAtaque:
                     if(!(refCard.Alvo == AlvoPassiva.CartaAtacada))
                    this.gameObject.GetComponent<EventControllerBehaviour>().RealizarPassivaEm(refCard.Passiva,refCard.Alvo,true,atacante.cartas[posicaoAtacar].gameObject);
                    else
                    this.gameObject.GetComponent<EventControllerBehaviour>().RealizarPassivaEm(refCard.Passiva,defensor.cartas[posicaoInimigo].gameObject,true,atacante.cartas[posicaoAtacar].gameObject);
                    // nao testei esse else

                 break;
                }
           atacante.cartas[posicaoAtacar].transform.GetComponent<Animator>().SetTrigger("Atacar");
           StartCoroutine(DarDano(defensor.cartas[posicaoInimigo],atacante.cartas[posicaoAtacar]));
    
        }
        
    }
    //player no caso é a vida dele e não os das suas cartas
    public void AtacarPlayer(int posicaoAtacador) 
    {
        MesaBehaviour defensor = transform.GetChild(2).GetComponent<MesaBehaviour>();
        defensor.cartas[posicaoAtacador].transform.GetChild(0).GetComponent<CartaNaMesa>().QuantidadeAtaque--;
        defensor.cartas[posicaoAtacador].transform.GetComponent<Animator>().SetTrigger("Atacar");
        StartCoroutine(DarDanoInimigo(defensor.cartas[posicaoAtacador]));
    }
    public void ColocarCartaBaralho(string id)
    {
        MesaBehaviour mesa = transform.GetChild(2).GetComponent<MesaBehaviour>();
        Card refCard =Resources.Load<Card>("InformacoesCartas/" + id);
        mesa.CriarCartaInicio(refCard.dano,refCard.vida,
        Resources.Load<Sprite>("CartasProntas/" + id),
        refCard.ativarPassivaQuando,
        new PassivaComulativa(refCard.quantidadePassiva,refCard.passiva),
        refCard.alvoDaPassiva,refCard.SomEmMorte);
        if(refCard.SomNaEntrada != null && refCard.SomNaEntrada.Length >0)
            GetComponent<Mao>().som.PlayOneShot(refCard.SomNaEntrada[Random.Range(0,refCard.SomNaEntrada.Length)]);
    }
    public void TirarCarta(int a)
    {
        
        CartaInimigo atributos = maoAdversaria[a].GetComponent<CartaInimigo>();
        MesaBehaviour mesa = transform.GetChild(2).GetComponent<MesaBehaviour>();
        CartaSegurada = maoAdversaria[a];    
        distanciamentoCartasMaximo -= 10;
        maoAdversaria[a].GetComponent<Animator>().SetBool("autoDestruir",true);
        maoAdversaria.RemoveAt(a);  
        SetAnimacao(distanciamentoCartasMaximo);   
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
        objCarta.transform.SetParent(transform.GetChild(3), false);
        objCarta.transform.localPosition += new Vector3(0, 400);  
        maoAdversaria.Add(objCarta);
        distanciamentoCartasMaximo += 10;
        SetAnimacao(distanciamentoCartasMaximo);
    }
    public void VoltarBaralho()
    {
        maoAdversaria.Insert(CartaSegurada.GetComponent<CartaInimigo>().PosicaoBaralho,CartaSegurada);
        distanciamentoCartasMaximo += 20;
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
    //animações dos efeitos
     Mao player = transform.GetComponent<Mao>();   
     yield return new WaitForSeconds(0.4f);
        if (obj && obj.transform.GetChild(0))
        {
            obj.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa -= atacante.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque;
            atacante.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa -= obj.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque;
        }
        player.Audio(2);
    }
    IEnumerator DarDanoInimigo(GameObject obj)
    {
    //efeitos
    yield return new WaitForSeconds(0.40f);
    Dano = Instantiate(dano);
    Dano.transform.SetParent(transform.GetChild(4), false);
    Dano.transform.localPosition = obj.transform.localPosition + Vector3.down * 50 + Vector3.left * 30;
    Dano.transform.localScale = new Vector3(3, 3);
    // IA de fato
    Dano.GetComponent<Text>().text += obj.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque.ToString();
    GetComponent<Mao>().TirarVidaPlayer(obj.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque);
    GetComponent<Mao>().Audio(2);
    }
    public void PerderVida(float dano) 
    {
        vida -= dano;
        vidaInimigo.text = vida + "/40";
    }
    public void SetarGold(int goldMax)
    {
        gold = goldMax;
        goldInimigo.text = string.Format("{0}/{1}",gold , goldMax);      
    }
}
