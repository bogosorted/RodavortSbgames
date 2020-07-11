using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Discovery;

public class botaoScript : MonoBehaviour
{
    public ServerResponse Info;
    public void OnClick()
    {
        transform.parent.GetComponent<Hud>().Connect(Info);
    }
}
