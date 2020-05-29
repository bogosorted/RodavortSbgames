using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exibicao : MonoBehaviour
{

   public void SetAtributos(string titulo,string descricao, string valor, string ataque,string vida, Sprite imagem)
   {
        Transform carta =transform.GetChild(0);
        transform.GetChild(1).GetComponent<Text>().text = titulo;
        transform.GetChild(2).GetComponent<Text>().text = descricao;
        carta.GetChild(0).GetComponent<Image>().sprite = imagem;
        carta.GetChild(2).GetComponent<Text>().text = vida;
        carta.GetChild(3).GetComponent<Text>().text = ataque;
        carta.GetChild(4).GetComponent<Text>().text = titulo;
        carta.GetChild(5).GetComponent<Text>().text = valor;

   }

}

