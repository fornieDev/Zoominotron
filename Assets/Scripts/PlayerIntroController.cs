using Unity.VisualScripting;
using UnityEngine;

//Clase que controla al jugador durante la cinemática
public class PlayerIntroController : MonoBehaviour
{
    //Variables
    private IntroductionTextManager introductionTextManagerScript;
    private Animator animator;
    private bool hasWin;
    private bool hasFailed;
    private bool isIdle;

    void Start()
    {
        //Inicialización
        introductionTextManagerScript = GameObject.Find("IntroductionText").GetComponent<IntroductionTextManager>();
        animator = GetComponent<Animator>();
        hasWin = false;
        hasFailed = false;
        isIdle = false;
    }

    void Update()
    {
        //En función del párrafo,se hacen ciertas acciones
        if (introductionTextManagerScript.index == 3 && !hasWin) 
        {
            //Hacer la animación de celebrar el experimento
            animator.SetTrigger("Win");
            hasWin = true;
        }
        if (introductionTextManagerScript.index == 4 && !hasFailed)
        {
            //Hace la animación de trabajar (se usa la animación de pickup para simularlo)
            animator.SetTrigger("Pickup");
            hasWin = true;
        }
        
        if (introductionTextManagerScript.index == 6 && !isIdle)
        {
            //Se queda estático el personaje
            animator.SetBool("isIdle", true);
            isIdle= true;
        }
        
        if (introductionTextManagerScript.index == 7)
        {
            //aumentar o disminuir el tamaño aleatoriamente cantidades peqeñas
            float number = Random.Range(0.9f, 1.1f);
            transform.localScale = new Vector3(number, number, number);
        }
        if (introductionTextManagerScript.index == 8)
        {
            //setarlo a un tamaño normal
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
