using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlharParaMouse : MonoBehaviour
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
        mousePos =Input.mousePosition;
        Vector3 objectPos = transform.position;
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -80));
        rect.sizeDelta = new Vector2( Mathf.Abs(mousePos.x)+ 300 * 1.5f,Mathf.Abs(mousePos.x) + mousePos.y).normalized *350;
        
    }
}
