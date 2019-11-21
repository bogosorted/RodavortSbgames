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
        switch (transform.GetChild(0).tag)
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
                Ataque =100;
                Vida = 100;
                break;
        }
        transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = Ataque.ToString();
        transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = Vida.ToString();

    }
    public void SubirCarta()
    {
       transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + 2);   
    }
    public void SilhuetaCarta(bool a)
    {
        if (a)
            transform.GetChild(0).GetComponent<Outline>().effectDistance = new Vector2(4.5f,4.5f);

    }
    public void Segurando(bool segurando)
    {
       cartaMao = segurando;
       transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,1));
    }

    private void Update()
    {
        
        if (cartaMao)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1));
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
      
        if (transform.localPosition.y > -270 && !cartaMao)
           transform.localPosition = new Vector2(transform.localPosition.x, -270);
    }
   
}
