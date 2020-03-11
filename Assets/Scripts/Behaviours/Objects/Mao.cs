using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Mao : MonoBehaviour
{
    public List<GameObject> mao = new List<GameObject>();
    [SerializeField] private GameObject carta;
    [SerializeField] Sprite[] ImagemCarta;
    float step,stepCarta;
    float x,xCarta;
    float max;
    GraphicRaycaster raycast;
    EventSystem input;
    GameObject CartaAtual;
    List<RaycastResult> resultados;
    PointerEventData cursor;

    void Update() 
    {
        Mouse();
    }
    public void Mouse()
    {
        cursor.position = Input.mousePosition;
        resultados = new List<RaycastResult>();
        raycast.Raycast(cursor, resultados);
        if (resultados.Count != 0 && resultados[0].gameObject.name == "Carta")
        {
            if (resultados[0].gameObject != CartaAtual || CartaAtual == null  )
            {
                SetAngulo(max);
                print("diferente");
                SetPosicao(resultados[0].gameObject);
            }
            CartaAtual = resultados[0].gameObject;
            
        }
    }
    private void SetPosicao(GameObject Carta)
    {
        Carta.GetComponentInParent<Carta>().PosicaoFinal = new Vector2(Carta.GetComponentInParent<Carta>().PosicaoFinal.x, Carta.GetComponentInParent<Carta>().PosicaoFinal.y + 75);
        InvokeRepeating("AngularCarta",0,Time.deltaTime);

    }
    public void SetCarta(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.GetComponent<Carta>().Constructor(id);
        objCarta.transform.SetParent(transform,false);
        objCarta.transform.localPosition += new Vector3(600, -290);  
        mao.Add(objCarta);
        max += 20;
        objCarta.GetComponent<Carta>().AngulacaoFinal = new Vector3(0, -90,-55);
        SetAngulo(max);
    }
    public void SetCartaTeste(int id)
    {
        GameObject objCarta = Instantiate(carta);
        objCarta.GetComponent<Carta>().Constructor(id);
        objCarta.transform.SetParent(transform, false);
        mao.Add(objCarta);
        max += 20;
        SetAnguloTeste(max);
        
    }  

    public void SetAnguloTeste(float max)
    {
        float angulacaoConst = mao.Count % 2 == 0f ? max / (float)(mao.Count / 2) : max / (float)((mao.Count - 1) / 2);
        float concatenador = -max;
         if (mao.Count == 1)
             concatenador = 0;            
         foreach(var y in mao)
        {
            y.GetComponent<Carta>().PosicaoInicial = y.transform.localPosition - Vector3.up*450;
            y.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador, -Mathf.Abs(concatenador)/3 -290);
            y.GetComponent<Carta>().AngulacaoFinal = (new Vector3(0, 0,-concatenador/10));

            concatenador += angulacaoConst;
            if (mao.Count % 2 == 0 && concatenador == 0) 
            {
                concatenador += angulacaoConst;
            }
                y.transform.SetSiblingIndex(-1);
        }
        InvokeRepeating("Angular",0,Time.deltaTime);
        x = 0;     
    }
    public void SetAngulo(float max) 
    {
        float angulacaoConst = mao.Count % 2 == 0f ? max / (float)(mao.Count / 2) : max / (float)((mao.Count - 1) / 2);
        float concatenador = -max;
        if (mao.Count == 1)
            concatenador = 0;
        foreach (var y in mao)
        {
            y.GetComponent<Carta>().PosicaoInicial = y.transform.localPosition;
            y.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador, -Mathf.Abs(concatenador) / 5 - 290);
            y.GetComponent<Carta>().AngulacaoInicial = y.GetComponent<Carta>().AngulacaoFinal;
            y.GetComponent<Carta>().AngulacaoFinal = new Vector3(0, 0, (-concatenador / 10) );

            // if cosmético
            if (concatenador == 0 || concatenador == max || concatenador == -max)
            {
                y.GetComponent<Carta>().PosicaoFinal = new Vector3(concatenador, -Mathf.Abs(concatenador) /5 -290);
            }
                concatenador += angulacaoConst;
            if (mao.Count % 2 == 0 && concatenador == 0)
            {
                concatenador += angulacaoConst;
            }
            y.transform.SetSiblingIndex(-1);
        }
        InvokeRepeating("Angular", 0, Time.deltaTime);
        x = 0;
    }

    
    private void Angular() 
    {
        step =  -x*x + 2*x;
        x += (0.3f * Time.deltaTime);
        if (x >= 1)
        {
            CancelInvoke("Angular");
            return;
        }
        foreach(var p in mao)
        {
            p.transform.localPosition = Vector2.Lerp(p.GetComponent<Carta>().PosicaoInicial, p.GetComponent<Carta>().PosicaoFinal, step);
            p.transform.eulerAngles = Vector3.Lerp(p.GetComponent<Carta>().AngulacaoInicial, p.GetComponent<Carta>().AngulacaoFinal, step);
        }

    }
    private void AngularCarta()
    {
        stepCarta = -xCarta*xCarta + 2*xCarta;
        xCarta += (0.4f * Time.deltaTime);
        if (xCarta >= 1) 
        {
            CancelInvoke("AngularCarta");
            return;
        }
    }
    public void AdicionarCarta(GameObject a) => mao.Add(a);
    void Start()
    {
        cursor = new PointerEventData(input);
        resultados = new List<RaycastResult>();
        raycast = GetComponent<GraphicRaycaster>();
        input = GetComponent<EventSystem>();
        SetCartaTeste(0);
        SetCartaTeste(0);
        InvokeRepeating("aa", 2, 2);
    }
    void aa() 
    {
        SetCarta(0);
    }
}
