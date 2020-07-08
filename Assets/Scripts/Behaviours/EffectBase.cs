using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usando mono behaviour só pra printar(tirar na versão final)
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
                     {4, ()=>new Nada()},
                     {3, ()=>new Matar()},
                     {1, ()=>new AtaqueConsecutivo()},
                     {2, ()=>new Curar()}, 
                     {0, ()=>new Nada()}
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
