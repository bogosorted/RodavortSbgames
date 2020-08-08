using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusicOnLoad : MonoBehaviour
{
    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag(this.gameObject.tag).Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
