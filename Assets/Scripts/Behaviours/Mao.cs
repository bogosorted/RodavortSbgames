using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Mao : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler ,IPointerExitHandler,IPointerEnterHandler
{
    [Header("Animações do Baralho")]
     [SerializeField] float velocidadeAnimacao = 1f;
     [SerializeField]float distancia = 1;
     [SerializeField] float indiceAngulacao = 12;
     [SerializeField] float altitude = -290 ;
     [SerializeField] float latitude = 0; 
    [Header("Configurações padrões")]
    //lembrar de colocar tudo

    [SerializeField]private Canvas canvas;
    [SerializeField] private GameObject carta;
    public List<GameObject> mao = new List<GameObject>();
    float x,y;   
    float distanciamentoCartasMaximo;
    GraphicRaycaster raycast;
    EventSystem input;
    Exibicao exibir;
    Animator OutPut;
    GameObject CartaAtual;
    List<RaycastResult> resultados;
    PointerEventData cursor;
    bool animarBaralho;
    bool entrar;

    void FixedUpdate() 
    {
        #if UNITY_STANDALONE || UNITY_EDITOR_WIN
         Mouse();
        #endif
        if(animarBaralho)
        {
         Angular();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
         CartaAtual = eventData.pointerCurrentRaycast.gameObject;
       if(CartaAtual != null && CartaAtual.name == "Carta(Clone)")
        {
        x=1;
        OutPut.SetBool("MouseNaCarta",false);
        mao.RemoveAt(CartaAtual.GetComponent<Carta>().PosicaoBaralho);
        distanciamentoCartasMaximo -=20;
        CartaAtual.name = "segurado";
         SetAnimacao(distanciamentoCartasMaximo);
        }      
      
    }
    public void OnDrag(PointerEventData eventData)
    {
       if(CartaAtual != null && CartaAtual.name == "segurado" )
        {
        CartaAtual.transform.position = Input.mousePosition ;
        CartaAtual.transform.parent.SetSiblingIndex(9);
        }      
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        if (CartaAtual != null  && CartaAtual.name == "segurado")
        {
         mao.Insert(CartaAtual.GetComponent<Carta>().PosicaoBaralho,CartaAtual);
         distanciamentoCartasMaximo +=20;
         CartaAtual.name = "Carta(Clone)";
        }
         SetAnimacao(distanciamentoCartasMaximo);
         OutPut.SetBool("MouseNaCarta",false);
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        CartaAtual = eventData.pointerCurrentRaycast.gameObject;
        entrar = true;      
        if (CartaAtual != null && CartaAtual.name == "Carta(Clone)")
        {
            OutPut.SetBool("MouseNaCarta",true);
            exibir.SetAtributos(CartaAtual.GetComponent<Carta>().Nome,CartaAtual.GetComponent<Carta>().Descricao,CartaAtual.GetComponent<Carta>().Valor.ToString(),CartaAtual.GetComponent<Carta>().Ataque.ToString(),CartaAtual.GetComponent<Carta>().Defesa.ToString(),CartaAtual.GetComponent<Carta>().Imagem);
            SetAnimacao(distanciamentoCartasMaximo);
            SetPosicao(eventData.pointerCurrentRaycast.gameObject,30,0);
        }
    } 
    public void OnEndDrag(PointerEventData eventData)
    {
        cursor.position = Input.mousePosition;
        resultados = new List<RaycastResult>();
        raycast.Raycast(cursor, resultados);
        //CartaAtual = eventData.pointerCurrentRaycast.gameObject;

        if( resultados.Count > 1 && resultados[resultados.Count - 1].gameObject.name == "CampoAmigo")
        {
            for(int i=0;i < resultados.Count - 1;i++)
            {
                if(resultados[i].gameObject.name != "Carta(Clone)" && resultados[i].gameObject.name != "CartaNaMesa")
                 {          
                     Carta atributos = resultados[i].gameObject.GetComponent<Carta>();
                     resultados[resultados.Count - 1].gameObject.GetComponent<MesaBehavior>().CriarCartaInicio(atributos.Ataque,atributos.Defesa,atributos.Imagem);
                     resultados[i].gameObject.name = "Destruido";
                     resultados[i].gameObject.GetComponent<Animator>().SetBool("Destruir",true);
                 }
             }
        }
        else if (CartaAtual != null && CartaAtual.name == "segurado")
        {
         mao.Insert(CartaAtual.GetComponent<Carta>().PosicaoBaralho,CartaAtual);
         distanciamentoCartasMaximo +=20;
         CartaAtual.name = "Carta(Clone)";
         SetAnimacao(distanciamentoCartasMaximo);         
        }
        
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
                    OutPut.SetBool("MouseNaCarta",true);
                    CartaAtual = resultados[0].gameObject;
                    Carta atributos = CartaAtual.GetComponent<Carta>();
                    exibir.SetAtributos(atributos.Nome,atributos.Descricao,atributos.Valor.ToString(),atributos.Ataque.ToString(),atributos.Defesa.ToString(),atributos.Imagem);
                    SetAnimacao(distanciamentoCartasMaximo);
                    SetPosicao(resultados[0].gameObject,30,0);
                }
            }
            else if (entrar)
            {
                OutPut.SetBool("MouseNaCarta",false);
                entrar = false;
            }    
        }
        
    }
    private void SetPosicao(GameObject Carta, float longitude , float latitude)
    {
        Carta atributos =  Carta.GetComponentInParent<Carta>();
        atributos.PosicaoFinal = new Vector2(atributos.PosicaoFinal.x + latitude, atributos.PosicaoFinal.y + longitude);
        atributos.AngulacaoFinal = Vector3.zero;
    }
    public void SetRaycast(bool result)
    {
        foreach(var obj in mao)
        {
            obj.GetComponent<Image>().raycastTarget = result;
        }
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
        int index = 0;
        if (mao.Count == 1)
            concatenador = 0;
        foreach (var obj in mao)
        {
            //setando ID da carta em relação ao baralho
            obj.GetComponent<Carta>().PosicaoBaralho = index;
            // Setando posição da carta final e inicial 
            obj.GetComponent<Carta>().PosicaoInicial = obj.transform.localPosition;
            obj.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador * distancia + latitude, -Mathf.Abs(concatenador) / 5 + altitude);
            // Setando a Angulação final e inicial
            obj.GetComponent<Carta>().AngulacaoInicial = obj.transform.GetChild(0).eulerAngles;
            obj.GetComponent<Carta>().AngulacaoFinal = new Vector3(0, 0, (-concatenador  / indiceAngulacao) );
            if (concatenador == 0 || concatenador == distanciamentoCartasMaximo || concatenador == -distanciamentoCartasMaximo)
                obj.GetComponent<Carta>().PosicaoFinal = new Vector3(concatenador * distancia + latitude, -Mathf.Abs(concatenador) /5 + altitude);
            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(index +3);
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
        foreach(var obj in mao)
        {
            Carta atributos =  obj.GetComponent<Carta>();
            atributos.AngulacaoInicial = (atributos.AngulacaoInicial.z > 180) ? atributos.AngulacaoInicial - Vector3.forward *360 : atributos.AngulacaoInicial;
            atributos.AngulacaoFinal = (atributos.AngulacaoFinal.z > 180) ? atributos.AngulacaoFinal - Vector3.forward *360 : atributos.AngulacaoFinal;
            obj.transform.localPosition = Vector2.Lerp(atributos.PosicaoInicial,atributos.PosicaoFinal, y);
            obj.transform.GetChild(0).eulerAngles = Vector3.Lerp(atributos.AngulacaoInicial, atributos.AngulacaoFinal,y);
        }

    }

    void Start()
    {
        OutPut = transform.GetChild(3).GetComponent<Animator>();
        exibir = transform.GetChild(3).GetComponent<Exibicao>();   
        cursor = new PointerEventData(input);
        resultados = new List<RaycastResult>();
        raycast = GetComponent<GraphicRaycaster>();
        input = GetComponent<EventSystem>();
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        CriarCartaInicio(Random.Range(0,13));
        //InvokeRepeating("aa",0,3);
    }
    void aa() 
    {
        CriarCarta(Random.Range(0,13));
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
             return;           
         foreach(var obj in mao)
        {
            Carta atributos = obj.GetComponent<Carta>();
            atributos.PosicaoInicial = obj.transform.localPosition - Vector3.up * 450;
            atributos.PosicaoFinal = new Vector2(concatenador * distancia + latitude, -Mathf.Abs(concatenador) / 5 + altitude);     
            atributos.AngulacaoFinal = new Vector3(0, 0, (-concatenador  / indiceAngulacao) );
            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(-1);
        }
       animarBaralho = true;
        x = 0;     
    }
#endregion
}
