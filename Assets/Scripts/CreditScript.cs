using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
 
    void Update()
    {
        if(Input.anyKeyDown)
            SceneManager.LoadScene("MenuInicial");
    }
}
