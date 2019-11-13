using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComportamentoPlayer : MonoBehaviour
{
    public List<GameObject> maoPlayerCampo = new List<GameObject>();
    RaycastHit2D mouse;
    bool segurando;
    public void AdicionarCartasPlayer(GameObject a)
    {
        maoPlayerCampo.Add(a);
    }
    void Update()
    {
        mouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector3.forward);
        if(mouse.collider != null)
        {
            if (mouse.collider.gameObject.name == "Image(Clone)")
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
                    b.transform.SetSiblingIndex(GetComponent<Deck>().mao.Count + 2);
                    segurando = true;
                }
                if (segurando)
                {               
                    mouse.collider.gameObject.GetComponent<Carta>().Segurando(segurando);
                    segurando = false;
                }                           
            }         
        }
        else
        {
            GetComponent<Deck>().SetAnguloZ(12);
            GetComponent<Deck>().DesfazerSilhuetas();  
        }
            
    }
}
