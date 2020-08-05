using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyMusicOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
