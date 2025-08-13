using UnityEngine;

//clase que mueve los créditos y carga el menú al finalizar éstos
public class Credits : MonoBehaviour
{
    //Variables
    private float speed = 100.0f;
    private RectTransform rectTransform;
    private SceneTransitionManager sceneTransitionManagerScript;

    void Start()
    {
        //Inicialización
        rectTransform = GetComponent<RectTransform>();
        sceneTransitionManagerScript = GameObject.Find("SceneTransitionBlack").GetComponent<SceneTransitionManager>();
    }

    void Update()
    {
        if (rectTransform != null)
        {
            //Mueve el texto de los créditos hacia arriba
            rectTransform.Translate(0, Time.deltaTime * speed, 0);
            //Cuando llegue a la posición,carga el menú inicial
            if (rectTransform.position.y > 2500)
            {
                StartCoroutine(sceneTransitionManagerScript.LoadSceneIndex(0));
            }
        }
    }
}
