using UnityEngine;

//Clase que controla la posición de la cámara
public class CameraController : MonoBehaviour
{
    //Variables
    private GameObject firstPersonCameraPoint;
    private GameObject thirdPersonCameraPoint;
    private DetectCollides detectCollidesScript;

    void Start()
    {
        //Inicialización
        firstPersonCameraPoint = GameObject.Find("FirstPersonCameraPoint");
        thirdPersonCameraPoint = GameObject.Find("ThirdPersonCameraPoint");
        detectCollidesScript = thirdPersonCameraPoint.GetComponent<DetectCollides>();
    }


    void Update()
    {
        //Si el punto en tercera persona colisiona,la cámara se coloca en primera persona,si no en tercera
        if (detectCollidesScript.thirdCameraIsColliding)
        {
            
            transform.position = firstPersonCameraPoint.transform.position;
            transform.rotation = firstPersonCameraPoint.transform.rotation;
        }
        else 
        {
            transform.position = thirdPersonCameraPoint.transform.position;
            transform.rotation = thirdPersonCameraPoint.transform.rotation;
        }
        //La rotación se podría sacar del if para eliminar una línea de código
    }
}
