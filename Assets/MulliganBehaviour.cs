using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;

public class MulliganBehaviour : NetworkBehaviour
{
    public GameObject carta;
    public GameObject negacao;

    PlayerId playerid;
    NetworkIdentity ntwrkid;

    List<GameObject> cartas = new List<GameObject>();
    
    GraphicRaycaster raycast;
    float x,y;
    EventSystem input;
    List<RaycastResult> resultados;
    PointerEventData cursor;

    

    bool animarBaralho;
    bool ativo,final;

    void Destroy()
    {
        Destroy(this.gameObject);
    }
    void Iniciou()
    {
        GetComponent<Image>().raycastTarget = true;
        transform.GetChild(0).GetComponent<Selectable>().interactable = true;

        ativo = true;     
    }
    void Awake()
    {
        resultados = new List<RaycastResult>();
        raycast = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        input = GameObject.Find("Canvas").GetComponent<EventSystem>();
        cursor = new PointerEventData(input);
        //CriarCarta(Random.Range())

       // Card refCard = Resources.Load<Card>("InformacoesCartas/" + numero);
       // Imagem = Resources.Load<Sprite>("CartasProntas/" + numero);  
    }
    public void CriarCarta(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.transform.localPosition -= Vector3.up *400;
        cartas.Add(objCarta);
        objCarta.GetComponent<Carta>().Constructor(id);
        objCarta.transform.SetParent(transform, false);
        objCarta.name = "MulliganCard";
        SetAnimacaoInicial(40);
       // Card refCard = Resources.Load<Card>("InformacoesCartas/" + numero);
       // Imagem = Resources.Load<Sprite>("CartasProntas/" + numero);  
    }
    public void OnMulliganClick()
    {
        transform.GetChild(0).GetComponent<Selectable>().interactable = false;

        ntwrkid = NetworkClient.connection.identity;
        playerid = ntwrkid.GetComponent<PlayerId>();

        Mao player = GameObject.Find("Canvas").GetComponent<Mao>();
        int rejeitados = 0;
        for(int i = 0; i != cartas.Count; i++)
        {
            print(i);
             if(cartas[i].transform.childCount != 1)
             {
                cartas[i].GetComponent<Animator>().SetBool("Destruir", true);
                player.DestruirCartaBaralho(player.mao[i]);
                player.distanciamentoCartasMaximo -= 20;
                player.mao.RemoveAt(i);
                playerid.CmdTirarCartaBaralho(i);
                cartas.RemoveAt(i);
                SetAnimacao(40);
                player.SetAnimacaoInicial(player.distanciamentoCartasMaximo);     
                rejeitados++;
                i--;  
               //  playerid
             }
             while (rejeitados != 0)
             {
                int rnd = Random.Range(0,13);
                playerid.CmdCriarCartaInicio(rnd);    
                CriarCarta(rnd);     
                SetAnimacao(40);     
                rejeitados--;
             }
             final = true;
           
        }
    }

    void Update()
    {
        if(!ativo)
            return;
        if(Input.GetMouseButtonDown(0)){
            cursor.position = Input.mousePosition;
            resultados = new List<RaycastResult>();
            raycast.Raycast(cursor, resultados);
            if (resultados.Count != 0)
            {              
                var obj =resultados[0].gameObject;
                if (obj.name == "MulliganCard" && !final)
                {
                    if(obj.transform.childCount != 1)
                        Destroy(obj.transform.GetChild(1).gameObject);
                    else
                        Instantiate(negacao,resultados[0].gameObject.transform);
              
                }
            }
        }
        if(animarBaralho)
        {
            Angular();
        }
    }
    
    private void SetAnimacaoInicial(float distanciamentoCartasMaximo)
    {
        float angulacaoConst = cartas.Count % 2 == 0f ? distanciamentoCartasMaximo / (float)(cartas.Count / 2) : distanciamentoCartasMaximo / (float)((cartas.Count - 1) / 2);     
        float concatenador = -distanciamentoCartasMaximo;
        int index = 0;
         if (cartas.Count == 1)
             concatenador = 0;        
         foreach(var obj in cartas)
        {
            
            Carta atributos = obj.GetComponent<Carta>();
            atributos.PosicaoBaralho = index;
            atributos.PosicaoInicial = obj.transform.localPosition - Vector3.up * 450;
            atributos.PosicaoFinal = new Vector2(concatenador * 5, -Mathf.Abs(concatenador) / 5 + 5 * 5 );     
            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(-1);
            index ++;
        }
        animarBaralho = true;
        x = 0;     
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
            obj.GetComponent<Carta>().PosicaoBaralho = index;
            obj.GetComponent<Carta>().PosicaoInicial = obj.transform.localPosition;
            obj.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador * 4, -Mathf.Abs(concatenador) / 5 + 25);

            if (concatenador == 0 || concatenador == distanciamentoCartasMaximo || concatenador == -distanciamentoCartasMaximo)
                obj.GetComponent<Carta>().PosicaoFinal = new Vector3(concatenador * 4, -Mathf.Abs(concatenador) / 5 + 25);
            concatenador += angulacaoConst;
            obj.transform.SetSiblingIndex(-1);
            index++;
        }
        animarBaralho = true;
        x = 0;
    }

    private void Angular() 
    {
        //função
        y = -x*x + 2*x;
        //velocidade da animação
        x += (Time.deltaTime);
        //dado o fim da animação
        if (x >= 1)
        {
            if(final)
            {
                GetComponent<Animator>().SetTrigger("Event");
                foreach(var obj in cartas)
                    obj.GetComponent<Animator>().SetBool("Destruir", true);
            }
            animarBaralho = false;
            return;   
        }
        //animando todas as cartas da mão
        //por meio do metodo vector.lerp
        foreach(var obj in cartas)
        {
            Carta atributos = obj.transform.GetComponent<Carta>();
            obj.transform.localPosition = Vector2.Lerp(atributos.PosicaoInicial, atributos.PosicaoFinal,y);
        }
    }
}
