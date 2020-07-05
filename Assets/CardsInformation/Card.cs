using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Efeitos
    {
        //atacar é um efeito comum a todas ascartas
        Nenhum,
        Curar,
        Executar
    }
   public class PassivaComulativa
    {
     public PassivaComulativa(int a,Efeitos b)
      {
       quantidade = a;
       efeito = b;
      }
      public void InfoAdd(int a , Efeitos b)
      {
        quantidade = a;
         efeito = b;
      }
        public Efeitos efeito;
        public int quantidade;
    }
    public enum Evento
    {
      //evite mudar um nome que ja haja programação
        Nunca,
        CartaIniciada,
        NovoRound,
        CartaAtaque,
        CartaMorreu,
    }
    public enum AlvoPassiva
    {
      //evite mudar um nome que ja haja programação
        Nenhum,
        TodasAsCartas,
        CartaAdversaria,
        CartaAliada,
        //nao fiz essa parte aq de baixo
        CartaAtacada,
        
    }

[CreateAssetMenu(menuName = "atributos da carta")]
public class Card : ScriptableObject
{
    public Efeitos passiva;
    public int quantidade;
    public Evento ativarPassivaQuando;
    public AlvoPassiva alvoDaPassiva; // por enquanto nem é passada
    // fim do teste
    public string nome;
    public string desc;
    public float dano;
    public float vida;
    public int valor;
}
