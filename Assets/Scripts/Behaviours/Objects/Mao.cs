using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Mao : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler ,IPointerExitHandler,IPointerEnterHandler
{
    [Header("Animações do Baralho")]
    public float velocidadeAnimacao = 1f;
    public float distancia = 1;
    public float indiceAngulacao = 12;
    public float altitude = -290 ;
    public float latitude = 0; 
    [Header("Configurações padrões")]
    //lembrar de colocar tudo

    [SerializeField]private Canvas canvas;
    [SerializeField] private GameObject carta;
    [SerializeField] Sprite[] ImagemCarta;
    public List<GameObject> mao = new List<GameObject>();
    float x,y;   
    float distanciamentoCartasMaximo;
    GraphicRaycaster raycast;
    EventSystem input;
    GameObject CartaAtual;
    List<RaycastResult> resultados;
    PointerEventData cursor;
    bool animarBaralho;
    bool entrar;
    void Update() 
    {
        Mouse();
        if(animarBaralho)
        {
            Angular();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
       if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name == "Carta")
       {
            //eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
       }      
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        SetAnimacao(distanciamentoCartasMaximo);
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        entrar = true;
        
        if (eventData.pointerCurrentRaycast.gameObject.name == "Carta(Clone)")
        {
            CartaAtual = eventData.pointerCurrentRaycast.gameObject;
            SetAnimacao(distanciamentoCartasMaximo);
            SetPosicao(eventData.pointerCurrentRaycast.gameObject);
        }
    } 
    public void OnEndDrag(PointerEventData eventData)
    {

    }
    public void Mouse()
    {
        cursor.position = Input.mousePosition;
        resultados = new List<RaycastResult>();
        raycast.Raycast(cursor, resultados);
        if (resultados.Count != 0)
        {
            if (resultados[0].gameObject.name == "Carta(Clone)")
            {
                if (resultados[0].gameObject != CartaAtual && entrar)
                {
                    SetAnimacao(distanciamentoCartasMaximo);
                    entrar = false;
                }
                else if (resultados[0].gameObject != CartaAtual)
                {
                    entrar = true;
                    CartaAtual = resultados[0].gameObject;
                    SetAnimacao(distanciamentoCartasMaximo);
                    SetPosicao(resultados[0].gameObject);
                }
            }
            else if (entrar)
            {
                SetAnimacao(distanciamentoCartasMaximo);
                entrar = false;
            }
        }
    }
    private void SetPosicao(GameObject Carta)
    {
        Carta.GetComponentInParent<Carta>().PosicaoFinal = new Vector2(Carta.GetComponentInParent<Carta>().PosicaoFinal.x, Carta.GetComponentInParent<Carta>().PosicaoFinal.y + 75);
        Carta.GetComponentInParent<Carta>().AngulacaoFinal = Vector3.zero;
    }
    public void CriarCarta(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.GetComponent<Carta>().Constructor(id);
        objCarta.transform.SetParent(transform,false);
        objCarta.transform.localPosition += new Vector3(600, -290);  
        mao.Add(objCarta);
        distanciamentoCartasMaximo += 20;
        objCarta.GetComponent<Carta>().AngulacaoFinal = new Vector3(0, -90,-55);
        SetAnimacao(distanciamentoCartasMaximo);
    }

    //Seta atributos da carta para realização da animação posteriormente
    //atributos simulam o distanciamento e angulação de um baralho em mãos 
    public void SetAnimacao(float distanciamentoCartasMaximo) 
    {
        // formula que leva em conta um valor de distancia do ponto 0 qualquer (distanciamentoDeCartaMaximo), e a quantidade de vezes
        // em que essa distancia é dividida igualmente (Quantidade de cartas). Devolvendo a constante de distanciamento (Levando em conta 
        // a imparidade ou paridade da divisão.

        //constante de distanciamento
        float angulacaoConst = mao.Count % 2 == 0f ? distanciamentoCartasMaximo / (float)(mao.Count / 2) : distanciamentoCartasMaximo / (float)((mao.Count - 1) / 2);
        //distancia inicial
        float concatenador = -distanciamentoCartasMaximo;
        if (mao.Count == 1)
            concatenador = 0;
        foreach (var obj in mao)
        {
            // Setando posição da carta final e inicial 
            obj.GetComponent<Carta>().PosicaoInicial = obj.transform.localPosition;
            obj.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador * distancia + latitude, -Mathf.Abs(concatenador) / 5 + altitude);
            // Setando a Angulação final e inicial
            obj.GetComponent<Carta>().AngulacaoInicial = obj.transform.GetChild(0).eulerAngles;
            obj.GetComponent<Carta>().AngulacaoFinal = new Vector3(0, 0, (-concatenador  / indiceAngulacao) );
            if (concatenador == 0 || concatenador == distanciamentoCartasMaximo || concatenador == -distanciamentoCartasMaximo)
                obj.GetComponent<Carta>().PosicaoFinal = new Vector3(concatenador * distancia + latitude, -Mathf.Abs(concatenador) /5 + altitude);
            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(-1);
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
        foreach(var obj in mao)
        {
            obj.GetComponent<Carta>().AngulacaoInicial = (obj.GetComponent<Carta>().AngulacaoInicial.z > 180) ? obj.GetComponent<Carta>().AngulacaoInicial - Vector3.forward *360 : obj.GetComponent<Carta>().AngulacaoInicial;
            obj.GetComponent<Carta>().AngulacaoFinal = (obj.GetComponent<Carta>().AngulacaoFinal.z > 180) ? obj.GetComponent<Carta>().AngulacaoFinal - Vector3.forward *360 : obj.GetComponent<Carta>().AngulacaoFinal;
            obj.transform.localPosition = Vector2.Lerp(obj.GetComponent<Carta>().PosicaoInicial, obj.GetComponent<Carta>().PosicaoFinal, y);
            obj.transform.GetChild(0).eulerAngles = Vector3.Lerp(obj.GetComponent<Carta>().AngulacaoInicial, obj.GetComponent<Carta>().AngulacaoFinal,y);
        }

    }

    void Start()
    {
        cursor = new PointerEventData(input);
        resultados = new List<RaycastResult>();
        raycast = GetComponent<GraphicRaycaster>();
        input = GetComponent<EventSystem>();
        CriarCartaInicio(0);
        CriarCartaInicio(1);
        CriarCartaInicio(2);
        CriarCartaInicio(3);
        CriarCartaInicio(4);
        CriarCartaInicio(5);
        CriarCartaInicio(6);
        CriarCartaInicio(7);
        //InvokeRepeating("aa",3,3);
    }
    void aa() 
    {
        CriarCarta(0);
    }
    #region Repeticoes    
    public void CriarCartaInicio(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.GetComponent<Carta>().Constructor(id);
        objCarta.transform.SetParent(transform, false);
        mao.Add(objCarta);
        distanciamentoCartasMaximo += 20;
        SetAnimacaoInicial(distanciamentoCartasMaximo);
        
    }  
    
    //Faz o mesmo que o SetAnimacao, porem tem uma animação diferenciada.
    //Feita com o unico intuito de ser rodada apenas no inicio (animação de receber as cartas iniciais).
    public void SetAnimacaoInicial(float distanciamentoCartasMaximo)
    {
        float angulacaoConst = mao.Count % 2 == 0f ? distanciamentoCartasMaximo / (float)(mao.Count / 2) : distanciamentoCartasMaximo / (float)((mao.Count - 1) / 2);
        float concatenador = -distanciamentoCartasMaximo;
         if (mao.Count == 1)
             concatenador = 0;            
         foreach(var obj in mao)
        {
            obj.GetComponent<Carta>().PosicaoInicial = obj.transform.localPosition - Vector3.up * 450;
            obj.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador * distancia + latitude , -Mathf.Abs(concatenador)/5 + altitude);
            obj.GetComponent<Carta>().AngulacaoFinal = (new Vector3(0, 0,-concatenador/indiceAngulacao));

            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(-1);
        }
       animarBaralho = true;
        x = 0;     
    }
#endregion
}
