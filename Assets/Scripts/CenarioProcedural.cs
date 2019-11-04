using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CenarioProcedural : MonoBehaviour
{
    [SerializeField] GameObject[] limitandos = new GameObject[4];
    void Start()
    {
      //  InvokeRepeating("Layer3", 3, 6);
        InvokeRepeating("Layer2", 3, 133);
        InvokeRepeating("Layer1", 2, 13);

    }


    void Layer1() {
        Instantiate(limitandos[0], new Vector3(gameObject.transform.position.x + 1100, gameObject.transform.position.y , 0), Quaternion.identity).transform.SetParent(gameObject.transform, false);
    }

    void Layer2() => Instantiate(limitandos[1], new Vector3(gameObject.transform.position.x + 1100, gameObject.transform.position.y , -100), Quaternion.identity).transform.SetParent(gameObject.transform, false);

   //void Layer3() => Instantiate(limitandos[2]).transform.SetParent(gameObject.transform, false);
    
}
