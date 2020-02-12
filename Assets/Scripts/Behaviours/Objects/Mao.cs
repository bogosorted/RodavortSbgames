using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mao : MonoBehaviour
{
    public List<GameObject> mao = new List<GameObject>();
    [SerializeField] private GameObject carta;
    [SerializeField] Sprite[] ImagemCarta;
    float step;
    float x;
    float max;
    
    public void SetCarta(int id)
    {
     GameObject objCarta = Instantiate(carta);
     objCarta.GetComponent<Carta>().Constructor(id);
     objCarta.transform.SetParent(transform,false);
     mao.Add(objCarta);
     max += 20;
     foreach(var x in mao)
        {
            x.GetComponent<Carta>().AngulacaoInicial = x.transform.eulerAngles;
        }
     SetAngulo(max);
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
            y.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador, -Mathf.Abs(concatenador)/3 -250);
            y.GetComponent<Carta>().AngulacaoFinal = (new Vector3(0, 0,-concatenador/10));

            concatenador += angulacaoConst;
            if (mao.Count % 2 == 0 && concatenador == 0) 
            {
                concatenador += angulacaoConst;
            }
                y.transform.SetSiblingIndex(3);
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
            y.GetComponent<Carta>().PosicaoFinal = new Vector2(concatenador, -Mathf.Abs(concatenador) / 3 - 250);
       //     y.GetComponent<Carta>().AngulacaoFinal = new Vector3(0, 0, max < 0 ? concatenador / 10 : Mathf.Abs(concatenador / 10));

            concatenador += angulacaoConst;
            if (mao.Count % 2 == 0 && concatenador == 0)
            {
                concatenador += angulacaoConst;
            }
            y.transform.SetSiblingIndex(3);
        }
        InvokeRepeating("Angular", 0, Time.deltaTime);
        x = 0;
    }

    private void Angular() 
    {
        step =  -x*x + 2*x;
        x += (0.4f * Time.deltaTime);
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
    //public void SetAnguloZ(int zMax)
    //{
             
    
    //    float angulacaoConst = mao.Count % 2 == 0f ? zMax / (float)(mao.Count / 2) : zMax / (float)((mao.Count - 1) / 2);
    //    float concatenador = -zMax;
    //    foreach (var x in mao)
    //    {
            
    //        x.transform.localPosition = new Vector3(concatenador * mao.Count * 3f, -Mathf.Abs(concatenador)*3 - 304);
    //        if (mao.Count == 1)
    //            concatenador = 0;        
    //        x.transform.GetChild(0).eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,-concatenador);
    //        if (concatenador == 0  || concatenador == zMax || concatenador == -zMax)
    //        {
    //            x.transform.localPosition = new Vector3(concatenador * mao.Count * 3f, -Mathf.Abs(concatenador) * 3 - 310);
    //        }
    //        concatenador += angulacaoConst;
    //        if (mao.Count % 2 == 0 && concatenador == 0)
    //        {
                      
    //            concatenador += angulacaoConst;
    //        }
    //        x.transform.SetSiblingIndex(-1);
    //    }
    //}
    public void AdicionarCarta(GameObject a)
    {
        mao.Add(a);
    }
    void Start()
    {
        InvokeRepeating("aa", 0, 2);
    }
    void aa() 
    {
        SetCarta(0);
    }
}
