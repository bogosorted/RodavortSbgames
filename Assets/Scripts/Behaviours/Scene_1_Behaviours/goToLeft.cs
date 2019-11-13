using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToLeft : MonoBehaviour
{
    private float velocity;
    void Start()
    {
        switch(gameObject.tag)
        {
            case "layer1":
                velocity = 1.5f;
                break;
            case "layer2":
                velocity = 0.5f;
                break;
            case "layer3":
                velocity = 0.225f;
                break;
        }
    }
    void Update()
    {
        RectTransform a = gameObject.GetComponent<RectTransform>();
        a.position += Vector3.left * velocity  * Time.deltaTime * 100 ;
        if (a.position.x < -1400)
            a.position = new Vector3(1600, a.position.y);
    }
}
