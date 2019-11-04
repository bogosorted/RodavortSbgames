using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class UI:MonoBehaviour
{
    [SerializeField] public Image[] TypesF;
    [SerializeField] private Canvas canvas;
    private Animator animator;
    private Image fade;

    /// <summary>
    /// Inicia o fade tipo 1, podendo ser IN ou OUT
    /// </summary>
    /// <param name="in_or_out">true para FADEIN,false para FADEOUT</param>
    /// <param name="seconds">velocidade do término em SEGUNDOS</param>
    /// /// <param name="type_fade">estilo do fade (Consultar Aragão)</param>
    public void Fades(bool in_or_out = true, float seconds = 1f, int type_fade = 1)
    {
        //4 types of fades actually
       if (fade != null)
           Destroy(fade);
        if (type_fade >= 0 && type_fade < TypesF.Length)
        {
            fade = Instantiate(TypesF[type_fade]);
        }
       fade.transform.SetParent(canvas.transform, false);
       fade.GetComponent<Animator>().SetBool("Ativator", in_or_out);
       if (seconds != 0)
         fade.GetComponent<Animator>().SetFloat("Seconds", 1 /seconds);
    }
}
