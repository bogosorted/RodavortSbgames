using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carta : MonoBehaviour
{
    LayerMask campoAmigo;
    GameObject campo;
    RaycastHit2D hit;
    Deck dono;
    private int Ataque, Vida;
    float suavidade = 0;
    float alturaY;
    bool cartaMao;

    public void SetDono(Deck a)
    {
        dono = a;
    }

    private void Start()
    {
        campoAmigo = LayerMask.GetMask("CampoAmigo");
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
            gameObject.GetComponent<Outline>().effectDistance = new Vector2(2.5f,2.5f);

    }
    public void Segurando(bool segurando)
    {
       cartaMao = segurando;
       GetComponent<Image>().rectTransform.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,1));
    }

    private void Update()
    {
        
        if (cartaMao)
        {
            GetComponent<Image>().rectTransform.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1));
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,campoAmigo);
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "CampoAmigo")
                {
                    campo = hit.collider.gameObject;
                    campo.GetComponent<Outline>().effectDistance = new Vector2(4, 4);
                }   
            }
            else if (campo != null)
                campo.GetComponent<Outline>().effectDistance = new Vector2(0, 0);
        }
        if (Input.GetMouseButtonUp(0) && cartaMao)
        {
            if (hit.collider != null && hit)
            {
                GetComponentInParent<ComportamentoPlayer>().AdicionarCartasPlayer(gameObject);
                Destroy(gameObject);
                campo.GetComponent<Outline>().effectDistance = new Vector2(0, 0);
            }
            else
            {
                dono.mao.Add(gameObject);
                dono.GetComponent<Deck>().SetAnguloZ(12);
            }
            cartaMao = false;
        }
        Image a = gameObject.GetComponent<Image>();
        if (a.rectTransform.anchoredPosition.y > -270 && !cartaMao)
            a.rectTransform.anchoredPosition = new Vector2(a.rectTransform.anchoredPosition.x, -270);
    }
   
}
