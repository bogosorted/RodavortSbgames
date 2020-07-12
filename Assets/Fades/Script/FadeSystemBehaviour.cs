using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystemBehaviour : MonoBehaviour
{
    // 
    public Image[] TypesF;
    //The instance of current fade (if exist)
    private Image currentFade;

    void Awake()
    {
        //example call method for the exemple scene
        CreateFade(true,1.5f,0);
    }
    /// <summary>      
    /// Starts a fade, either in or out,and being able to choose its duration.
    /// By gafds (Gabriel Aragão Ferreira da Silva)
    /// </summary>
    /// <param name="in_or_out">true to FADEIN,false to FADEOUT</param>
    /// <param name="seconds">velocity in seconds</param>
    /// /// <param name="type_fade">style of fade</param>
    public void CreateFade(bool in_or_out = true, float seconds = 1f, int type_fade = 0)
    {
        if (currentFade != null)
            Destroy(currentFade);
        if (type_fade >= 0 && type_fade < TypesF.Length)
            currentFade = Instantiate(TypesF[type_fade]);
        // the parent has
        currentFade.transform.SetParent(this.transform, false);
        currentFade.gameObject.GetComponent<Animator>().SetBool("Ativator", in_or_out);
        currentFade.gameObject.GetComponent<Animator>().SetFloat("Seconds", 1 /seconds);
    }
}
