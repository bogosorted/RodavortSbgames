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
    
    public abstract void RealizarEfeitoEm(List<GameObject> a);
    public abstract void RealizarEfeitoEm(GameObject a);
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
                     {3, ()=>new Nada()},
                     {1, ()=>new Curar()},
                     {2, ()=>new Matar()}, 
                     {0, ()=>new Nada()}
    };  
}


public class Curar:EffectBase
{   
     public override void RealizarEfeitoEm(List<GameObject> a)
    {
        for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i]);
    }
   
    public override void RealizarEfeitoEm(GameObject a)
    {
        a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa += quantidadeDoEfeito;
    }
}
public class Matar:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a)
    {
        for(int i = a.Count - 1 ; i >= 0;i--)
            RealizarEfeitoEm(a[i]);
    }
   
    public override void RealizarEfeitoEm(GameObject a)
    {
      a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa -= a.transform.GetChild(0).GetComponent<CartaNaMesa>().Defesa;
    }
}
// clase temporaria e sera removida na versão final
public class Nada:EffectBase
{
    public override void RealizarEfeitoEm(List<GameObject> a)
    {
       //absolutamente nada
    }
   
    public override void RealizarEfeitoEm(GameObject a)
    {
       // nadica
    }
}
