using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{

    private int Ataque, Vida;
    Deck dono;
    float suavidade = 0;
    float alturaY;
    bool cartaMao;

    public void SetDono(Deck a)
    {
        dono = a;
    }

    private void Start()
    {
        switch (tag)
        {
            case "Trovador":
                Ataque = 15;
                Vida = 13;
                break;
            case "Bardo":
                Ataque = 5;
                Vida = 6;
                break;
            case "Professor":
                Ataque = 0;
                Vida = 0;
                break;
        }
       
    }
    public void SubirCarta()
    {
       Image a = gameObject.GetComponent<Image>();
       a.rectTransform.anchoredPosition = new Vector2(a.rectTransform.anchoredPosition.x, a.rectTransform.anchoredPosition.y + 2);
       
    }
    public void SilhuetaCarta(bool a)
    {
        if (a)
            gameObject.GetComponent<Outline>().effectDistance = new Vector2(5, -5);

    }
    public void Segurando()
    {
       cartaMao = true;
       GetComponent<Image>().rectTransform.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, +1));
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && cartaMao)
        {
            dono.mao.Add(gameObject);
            cartaMao = false;
        }
            
        if (cartaMao)
            GetComponent<Image>().rectTransform.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, +1));
        Image a = gameObject.GetComponent<Image>();
        if (a.rectTransform.anchoredPosition.y > -270 && !cartaMao)
            a.rectTransform.anchoredPosition = new Vector2(a.rectTransform.anchoredPosition.x, -270);
    }
   
}
