using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exibicao : MonoBehaviour
{

   public void SetAtributos(string titulo,string descricao, string valor, string ataque,string vida, Sprite imagem)
   {
        transform.GetChild(1).GetComponent<Text>().text = titulo;
        transform.GetChild(2).GetComponent<Text>().text = descricao;
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = imagem;
        transform.GetChild(0).GetChild(2).GetComponent<Text>().text = vida;
        transform.GetChild(0).GetChild(3).GetComponent<Text>().text = ataque;
        transform.GetChild(0).GetChild(4).GetComponent<Text>().text = titulo;
        transform.GetChild(0).GetChild(5).GetComponent<Text>().text = valor;

   }

}

