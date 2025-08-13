using UnityEngine;

//Clase que sirve de listener para indicar si el punto de 3 persona colisiona o no
public class DetectCollides : MonoBehaviour
{
    //Variables
    public bool thirdCameraIsColliding;
    private GameObject firstPersonCameraPoint;
    private Vector3 offset;

    void Start()
    {
        //Inicialización
        thirdCameraIsColliding = false;
        firstPersonCameraPoint = GameObject.Find("FirstPersonCameraPoint");
        offset = new Vector3(0, 0.2f, -1.7f);
    }

    void Update()
    {
        //Coloca el punto de tercera persona por detrás del jugador
        Vector3 desiredPosition = firstPersonCameraPoint.transform.position + firstPersonCameraPoint.transform.TransformDirection(offset);
        transform.position = desiredPosition;
        transform.LookAt(firstPersonCameraPoint.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        //Mientras el punto está colisionando,el booleano es true
        thirdCameraIsColliding = true;
    }

    

    private void OnTriggerExit(Collider other)
    {
        //Cuando deje de colisionar,el booleano es falso
        thirdCameraIsColliding = false;
    }
}
