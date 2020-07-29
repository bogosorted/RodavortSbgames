using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaEfeitoBehaviour : MonoBehaviour
{
    Vector3 mousePos;
    RectTransform rect;
    private void Start() {
        rect = transform.GetComponent<RectTransform>();
    }
    public void autoDestruir()
    {
        Destroy(this.gameObject);
    }
    void Update()
    {
        
        if(Input.GetMouseButtonUp(0))
        {
          transform.parent.transform.parent.GetComponent<Mao>().EfeitoCancelado(); 
          GetComponent<Animator>().SetBool("SetaDestruir",true);
        }
        mousePos =Input.mousePosition;
        Vector3 objectPos = transform.position;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -90));
        rect.sizeDelta = new Vector2( Mathf.Abs(mousePos.x * 0.05f) + mousePos.y * 0.2f , Mathf.Abs(mousePos.x)* 0.3f + mousePos.y *0.6f);
    }
}
