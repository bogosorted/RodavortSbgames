using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoDano : MonoBehaviour
{
    Rigidbody2D rdgy;
    float xForce, yForce;
    void Start()
    {
        xForce = 50;
        yForce = 30;
        rdgy = GetComponent<Rigidbody2D>();
        rdgy.AddForce(new Vector2(Random.Range(-50, 51)*yForce, 100*xForce));
        StartCoroutine(Esperar());
    }
    IEnumerator Esperar() 
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
