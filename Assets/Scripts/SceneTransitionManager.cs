using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase para generar la transición entre escenas
public class SceneTransitionManager : MonoBehaviour
{
    //Variables comunes a todas las transiciones
    private Animator animator;
    [SerializeField] private AnimationClip animationOut;
    private string sceneName;
    //Variables específicas (sólo funcionan en alguna escena)
    public bool animationTrigger;
    private IntroductionTextManager introductionTextManagerScript;
    private bool animationLaunched;

    void Start()
    {
        //Inicializaciones comunes a todas las transiciones
        animator = GetComponent<Animator>();
        sceneName = SceneManager.GetActiveScene().name;

        //Inicializaciones de variables específicas de alguna escena
        switch (sceneName) 
        {
            case "MenuScene":
                animationTrigger = false;
                break;
            case "IntroScene":
                introductionTextManagerScript = GameObject.Find("IntroductionText").GetComponent<IntroductionTextManager>();
                animationLaunched = false;
                break;
            default:
                //Escena no recogida en el switch
                break;
        }
    }


    void Update()
    {
        //En función de la escena,se hace una lógica u otra para lanzar la transición
        switch (sceneName)
        {
            case "MenuScene":
                if (animationTrigger)
                {
                    StartCoroutine(LoadScene());
                }
                break;
            case "IntroScene":
                if (introductionTextManagerScript.index == 11 && !animationLaunched)
                {
                    animationLaunched = true;
                    StartCoroutine(WaitAndLoadScene());
                }
                break;
            default:
                //Escena no recogida en el switch
                break;
        }
    }

    IEnumerator WaitAndLoadScene()
    {
        //Corrutina que espera y carga la siguiente escena
        yield return new WaitForSeconds(8);
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        //corrutina que hace la animación de transición y carga la siguiente escena
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(animationOut.length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator LoadSceneIndex(int index)
    {
        //corrutina que hace la animación de transición y carga la escena que se pasa por parámetro
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(animationOut.length);

        SceneManager.LoadScene(index);
    }
}
