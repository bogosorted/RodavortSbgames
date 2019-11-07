using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    bool keyDown;
    UI UI;
    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<UI>();
        UI.Fades(true ,3f, Random.Range(0,3));
    }

    // Update is called once per frame
    void Update()
    {
        print(Time.time);
        if (!keyDown && Input.anyKeyDown && Time.time > 5)
            StartCoroutine("Temporizador");
    }
     IEnumerator Temporizador()
    {
        UI.Fades(false, 3f, Random.Range(0, 2));
        keyDown = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(1);

    }
}
