using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//Clase deprecada (se creó una clase sceneTransitionManager para generalizar los métodos y fusionar sceneTransition y sceneTransition2)
public class SceneTransition : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip animationOut;

    public bool animationTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        animationTrigger = false;

        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current scene: " + currentScene.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTrigger) 
        {
            StartCoroutine(loadScene());
        }
    }

    IEnumerator loadScene() 
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(animationOut.length);

        SceneManager.LoadScene("IntroScene");
    }
}
