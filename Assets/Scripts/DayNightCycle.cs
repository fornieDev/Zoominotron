using UnityEngine;

//clase para girar la luz direccional del menú
public class DayNightCycle : MonoBehaviour
{
    //Variables
    [SerializeField] private float speed;

    void Start()
    {
        //Inicialización
        speed = 1.0f;
    }

    void Update()
    {
        //Rota la luz direccional
        transform.Rotate(Vector3.right * speed * Time.deltaTime);
    }
}
