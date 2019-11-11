using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComportamentoPlayer : MonoBehaviour
{
    GameObject[,] Inimigo = new GameObject[2,3];
    GameObject[,] Player = new GameObject[2,3];
    RaycastHit2D mouse;
    bool segurando;
   /* public void AdicionarCartasPlayer(GameObject a, int x , int y)
    {
      Player[x, y] = a;
    }*/
    void Update()
    {
        mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward);
        if(mouse.collider != null)
        {
            var a = mouse.collider.gameObject.GetComponent<Carta>();
            GetComponent<Deck>().DesfazerSilhuetas();
            a.SilhuetaCarta(true);
            a.SubirCarta();
            if (Input.GetMouseButtonDown(0) && !segurando)
            {
                GameObject b = mouse.collider.gameObject;
                GetComponent<Deck>().mao.Remove(b);
                GetComponent<Deck>().SetAnguloZ(12);
                segurando = true;
            }
            if (segurando)
            {
                mouse.collider.gameObject.GetComponent<Carta>().Segurando();
                segurando = false;
            }      
        }
        else
        {
            GetComponent<Deck>().SetAnguloZ(12);
            GetComponent<Deck>().DesfazerSilhuetas();  
        }
            
    }
}
