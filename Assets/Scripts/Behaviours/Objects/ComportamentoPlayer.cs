using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComportamentoPlayer : MonoBehaviour
{
    public List<GameObject> maoPlayerCampo = new List<GameObject>();
    RaycastHit2D mouse;
    GameObject c;
    bool segurando;
    public void AdicionarCartasPlayer(GameObject a)
    {
        maoPlayerCampo.Add(a);
    }
    void Update()
    {

   
            
    }
}
