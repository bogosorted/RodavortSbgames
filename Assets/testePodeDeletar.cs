using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testePodeDeletar : MonoBehaviour
{

 
    void Update()
    {
        transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.z - 1);
        print(transform.eulerAngles);
    }
}
