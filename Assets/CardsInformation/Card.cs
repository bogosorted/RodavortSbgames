using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Efeitos
    {
        //atacar é um efeito comum a todas ascartas
        Nenhum,
        //se for usada quando a carta é iniciada ela ja nasce com possibilidade de atacar
        AtaqueConsecutivo,
        // a cura pode ser negativa bobao
        Curar,
        Matar,
        AprimorarAtaque,
        //aumenta tanto a vida quando o ataque
        AprimoracaoTotal,
        Executar,
       
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
        CartaRecebeuDano,
    }
    public enum AlvoPassiva
    {
      //evite mudar um nome que ja haja programação
        Nenhum,
        TodasAsCartas,
        CartaAdversaria,
        CartaAliada,
        CartaAleatoriaAliada,
        CartaAleatoriaAdversaria,
        Carta,
        //quando o player 1 ataca o player 2, a carta do player 1 ativaria a passiva.
        //só funcionam em seus respectivos eventos :  CartaAtaque e CartaRecebeuDano
        CartaAtacada,
        CartaQueAtacou,
    }

[CreateAssetMenu(menuName = "atributos da carta")]
public class Card : ScriptableObject
{
    public Efeitos passiva;
    public Evento ativarPassivaQuando;
    public AlvoPassiva alvoDaPassiva; // por enquanto nem é passada
    public int quantidadePassiva;
    // fim do teste
    public string nome;
    public string desc;
    public float dano;
    public float vida;
    public int valor;
    public AudioClip[] SomNaEntrada;
    public AudioClip[] SomEmMorte;
}
