using UnityEngine;

//clase para controlar la animación de la explosión en la cinemática
public class Explosion : MonoBehaviour
{
    //Variables
    private IntroductionTextManager introductionTextManagerScript;
    private Animator animator;
    private bool hasExplode;
    private AudioSource explosionSound;

    void Start()
    {
        //inicialización
        introductionTextManagerScript = GameObject.Find("IntroductionText").GetComponent<IntroductionTextManager>();
        animator = GetComponent<Animator>();
        hasExplode = false;
        explosionSound = GetComponent<AudioSource>();
    }

 
    void Update()
    {
        //Inicia la animación de la explosión cuando la narración va por el párrafo 6
        if (introductionTextManagerScript.index == 6 && !hasExplode) 
        {
            animator.SetTrigger("Explode");
            explosionSound.Play();
            hasExplode = true;
        }
    }
}
