using System.Collections;
using UnityEngine;

//Clase para animar el rayo
public class Ray : MonoBehaviour
{
    //Variables
    private IntroductionTextManager introductionTextManagerScript;
   
    void Start()
    {
        //Inicialización
        introductionTextManagerScript = GameObject.Find("IntroductionText").GetComponent<IntroductionTextManager>();
    }

    void Update()
    {
        //activa el rayo cuando la narración llega al párrafo 6
        if (introductionTextManagerScript.index == 6)
        {
            gameObject.SetActive(false);
        }
    }
}
