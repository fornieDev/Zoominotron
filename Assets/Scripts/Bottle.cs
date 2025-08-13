using UnityEngine;

//Clase para controlar la animación de la botella en la cinemática
public class Bottle : MonoBehaviour
{
    //Variables
    private IntroductionTextManager introductionTextManagerScript;
    private Animator animator;
    private bool contractTrigger;
    
    void Start()
    {
        //Inicialización de variables
        introductionTextManagerScript = GameObject.Find("IntroductionText").GetComponent<IntroductionTextManager>();
        animator = GetComponent<Animator>();
        contractTrigger = false;
    }

    
    void Update()
    {
        //Inicia la animación de la botella cuando la naracción va por el párrafo 2
        if (introductionTextManagerScript.index == 2 && !contractTrigger) 
        {
            animator.SetTrigger("Contract");
            contractTrigger = true;
        }
    }
}
