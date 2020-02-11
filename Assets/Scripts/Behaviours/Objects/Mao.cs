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
    
    public void SetCarta(int id)
    {
     GameObject objCarta = Instantiate(carta);
     objCarta.GetComponent<Carta>().Constructor(id);
     objCarta.transform.SetParent(transform,false);
     mao.Add(objCarta);
    }

         public void SetAnguloTeste(float max)
    {
        float angulacaoConst = mao.Count % 2 == 0f ? max / (float)(mao.Count / 2) : max / (float)((mao.Count - 1) / 2);
        float concatenador = -max;
         if (mao.Count == 1)
             concatenador = 0;            
         foreach(var y in mao)
        {
            y.GetComponent<Carta>().AngulacaoInicial = y.transform.localPosition - Vector3.up*450;
            y.GetComponent<Carta>().AngulacaoFinal = new Vector2(concatenador, -Mathf.Abs(concatenador)/3 -250);

            concatenador += angulacaoConst;
            if (mao.Count % 2 == 0 && concatenador == 0) 
            {
                concatenador += angulacaoConst;
            }
                y.transform.SetSiblingIndex(3);
        }
        InvokeRepeating("Angular",0,Time.deltaTime);
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
            p.transform.localPosition = Vector2.Lerp(p.GetComponent<Carta>().AngulacaoInicial, p.GetComponent<Carta>().AngulacaoFinal, step);
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
        SetCarta(0);
        SetCarta(0);
        SetCarta(0);
        SetCarta(0);
        SetAnguloTeste(160);
    }
}
