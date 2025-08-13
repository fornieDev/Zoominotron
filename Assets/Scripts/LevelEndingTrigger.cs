using UnityEngine;

//Clase para cargar la siguiente escena cuando se activa el trigger
public class LevelEndingTrigger : MonoBehaviour
{
    //Variables
    private SceneTransitionManager sceneTransitionManagerScript;
    private Rigidbody playerRigidBody;
    private DataPersist dataPersistScript;


    void Start()
    {
        //Inicialización
        sceneTransitionManagerScript = GameObject.Find("SceneTransitionBlack").GetComponent<SceneTransitionManager>();
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        dataPersistScript = GameObject.Find("DataPersist").GetComponent<DataPersist>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Hay que borrar dataPersist por si se cambia de nivel (si no al pasar al sigguiente nivel carga el mismo).
        //Con solo cambiar el map index es suficiente
        dataPersistScript.mapIndex = 0;
        //se congela el jugador y se carga la siguiente escena
        playerRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        StartCoroutine(sceneTransitionManagerScript.LoadScene());
    }
}
