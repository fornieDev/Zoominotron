using UnityEngine;

//Esta es una clase con un patrón singleton para persistir los datos entre escenas
public class DataPersist : MonoBehaviour
{
    //Variables
    public static DataPersist instance;
    //Datos
    public int mapIndex;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 playerScale;
    public float velocity;
    public float jumpForce;
    public string dateTime;


    private void Awake()
    {
        //Instancia de singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //Guarda el objeto con los datos para que no se destruya al cambiar de escena
        DontDestroyOnLoad(gameObject);
    }
}
