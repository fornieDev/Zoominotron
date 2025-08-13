using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Clase deprecada (se creó una clase sceneTransitionManager para generalizar los métodos y fusionar sceneTransition y sceneTransition2)
public class SceneTransition2 : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip animationOut;

    private IntroductionTextManager introductionTextManagerScript;
    private bool animationLaunched;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        introductionTextManagerScript = GameObject.Find("IntroductionText").GetComponent<IntroductionTextManager>();
        animationLaunched = false;

        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current scene: " + currentScene.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (introductionTextManagerScript.index == 11 && !animationLaunched)
        {
            animationLaunched = true;
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene() 
    {
        yield return new WaitForSeconds(8);
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(animationOut.length);

        SceneManager.LoadScene("RoomScene");
    }
}
