﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public List<GameObject> mao = new List<GameObject>();
    [SerializeField] private GameObject carta;
    [SerializeField] Sprite[] ImagemCarta;
    
   
    public void SetCarta(int id)
    {
        if (mao.Count != 9)
        {
            GameObject a = Instantiate(carta);
            a.transform.SetParent(transform, false);
            a.AddComponent<Carta>();
            mao.Add(a);
            a.GetComponent<Carta>().SetDono(this);

            switch (id)
            {
                case -1:
                    a.transform.GetChild(0).GetComponent<Image>().sprite = ImagemCarta[id];
                    break;
                case 0:
                    a.transform.GetChild(0).tag = "Trovador";
                    goto case -1;
                case 1:
                    a.transform.GetChild(0).tag = "Bardo";
                    goto case -1;
                case 2:
                    a.transform.GetChild(0).tag = "Professor";
                    goto case -1;


            }
            SetAnguloZ(12);
        }
    }
    public void SetAnguloZ(int zMax)
    {
        float angulacaoConst = mao.Count % 2 == 0f ? zMax / (float)(mao.Count / 2) : zMax / (float)((mao.Count - 1) / 2);
        float concatenador = -zMax;
        
        foreach (var x in mao)
        {
            x.transform.localPosition = new Vector3(concatenador * mao.Count * 3f, -Mathf.Abs(concatenador)*3 - 304);
            if (mao.Count == 1)
                concatenador = 0;        
            //angulação Z retirada
            x.transform.GetChild(0).eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,-concatenador);
            if (concatenador == 0  || concatenador == zMax || concatenador == -zMax)
            {
                x.transform.localPosition = new Vector3(concatenador * mao.Count * 3f, -Mathf.Abs(concatenador) * 3 - 310);
            }
            concatenador += angulacaoConst;
            if (mao.Count % 2 == 0 && concatenador == 0)
            {
                      
                concatenador += angulacaoConst;
            }
            x.transform.SetSiblingIndex(-1);


        }
    }
    public void DesfazerSilhuetas()
    {
        foreach (var x in mao)
        {
            x.transform.GetChild(0).GetComponent<Outline>().effectDistance = new Vector2(0, 0);
        }
    }  
    public void AdicionarCarta(GameObject a)
    {
        mao.Add(a);
    }
    void Start()
    {
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
        SetCarta(Random.Range(0, 3));
    }
}
