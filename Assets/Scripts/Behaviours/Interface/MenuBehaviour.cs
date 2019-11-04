using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        UI UI = GetComponent<UI>();
        UI.Fades(true ,3f, Random.Range(0,3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
