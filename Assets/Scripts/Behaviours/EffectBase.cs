using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectBase
{
    protected Efeitos efeito {get;set;}
    protected Evento evento{get;set;}
    protected AlvoPassiva alvo {get;set;}
    public int quantidadeDoEfeito{get;set;}
    
    public abstract void RealizarEfeitoEm(List<GameObject> realizado,GameObject objetoRealizador);
    public abstract void RealizarEfeitoEm(GameObject realizado,GameObject objetoRealizador);
}


public static class Factory
{
    public static EffectBase Criar(int id)
    {   
        var factory = cardFactories[id];
        return factory();
    }
     public static  Dictionary<int, Func<EffectBase>> cardFactories = new Dictionary<int, Func<EffectBase>>{  
                     {0, ()=>new Nada()},
                     {1, ()=>new AtaqueConsecutivo()},
                     {2, ()=>new Curar()}, 
                     {3, ()=>new Matar()},
                     {4, ()=>new AprimorarAtaque()},
                     {5, ()=>new AprimoracaoTotal()},
                     {6, ()=>new Executar()
                     }
    };  
}


public class Curar:EffectBase
{   
     public override void RealizarEfeitoEm(List<GameObject> a, GameObject b)
    {
        for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i],b);
    }
   
    public override void RealizarEfeitoEm(GameObject a,  GameObject b)
    {
        a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa += quantidadeDoEfeito;
    }
}
public class Matar:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a ,GameObject b)
    {
        for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i],b);
    }
   
    public override void RealizarEfeitoEm(GameObject a, GameObject b)
    {
      a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa -= a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa;
    }
}
public class AtaqueConsecutivo:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a , GameObject b)
    {      
        for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i],b);
    }
   
    public override void RealizarEfeitoEm(GameObject a ,  GameObject b)
    {
       a.transform.GetChild(0).GetComponent<CartaNaMesa>().QuantidadeAtaque += quantidadeDoEfeito;
    }
}
public class AprimorarAtaque:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a ,  GameObject b)
    {
       for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i],b);
    }
   
    public override void RealizarEfeitoEm(GameObject a ,  GameObject b)
    {
          a.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque += quantidadeDoEfeito;
    }
}
public class AprimoracaoTotal:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a ,  GameObject b)
    {
       for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i],b);
    }
   
    public override void RealizarEfeitoEm(GameObject a ,  GameObject b)
    {
        a.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque += quantidadeDoEfeito;
        a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa += quantidadeDoEfeito;
    }
}
public class Executar:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a ,  GameObject b)
    {
       for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i],b);
    }
   
    public override void RealizarEfeitoEm(GameObject a ,  GameObject b)
    {
        //Executa um inimigo com menor ou igual a quantidade do efeito
        if(a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa - (quantidadeDoEfeito + b.transform.GetChild(0).GetComponent<CartaNaMesa>().Ataque) <= 0) 
            a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa -= quantidadeDoEfeito;

    }
}
// clase temporaria e sera removida na versão final
public class Nada:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a ,  GameObject b)
    {
       //absolutamente nada
    }
   
    public override void RealizarEfeitoEm(GameObject a ,  GameObject b)
    {
       // nadica
    }
}
