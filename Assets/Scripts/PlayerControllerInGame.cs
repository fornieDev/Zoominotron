using UnityEngine;

//Clase para controlar al jugador
public class PlayerControllerInGame : MonoBehaviour
{
    //Componentes
    private Rigidbody rigidbody;
    private Animator animator;
    //Variables externas
    [SerializeField] public float velocity;
    [SerializeField] public float jumpForce;
    //Variables internas
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private bool isOnGround;
    [SerializeField] private float parameterModifier;
    [SerializeField] private float smallLimit;
    [SerializeField] private float bigLimit;
    private float rotationSpeed;
    private DataPersist dataPersistScript;

    void Start()
    {
        //Inicialización
        dataPersistScript = GameObject.Find("DataPersist").GetComponent<DataPersist>();
        //Cuando la partida haya sido cargada,setea el estado de jugador con esos datos.Si no,se quedan por defecto
        if (dataPersistScript.mapIndex != 0)
        {
            //Setea al jugador con los datos cargados
            transform.position = dataPersistScript.playerPosition;
            transform.rotation = dataPersistScript.playerRotation;
            transform.localScale = dataPersistScript.playerScale;
            velocity = dataPersistScript.velocity;
            jumpForce = dataPersistScript.jumpForce;
        }
        else
        {
            //Inicializa al jugador con valores por defecto
            velocity = 5.0f;
            jumpForce = 5.0f;
        }
        //Inicializacion
        rigidbody = GetComponent<Rigidbody>();
        rotationSpeed = 120.0f;
        isOnGround = true;
        animator = GetComponent<Animator>();
        parameterModifier = 3;
        smallLimit = 0.1f;
        bigLimit = 3.1f;
}


    void Update()
    {
        //checkea si se está pulsando Space o Q o E y se llama a los métodos jump o modifyScale
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround){ jump(); }
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)) { modifyScale(); }
    }

    private void FixedUpdate()
    {
        //Checkea si se está pulsando WASD y llama a los métodos rotate o move
        move(verticalInput = Input.GetAxis("Vertical"));
        rotate(Input.GetAxis("Horizontal"));
    }

    private void jump() 
    {
        //Método para saltar y animar al personaje
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        animator.SetBool("isOnGround", false);
        animator.SetTrigger("jump");
    }

    private void move(float verticalInput) 
    {
        //Método para mover al personaje hacia delante y detrás y animarlo
        //Mueve al personaje
        Vector3 movement = transform.forward * verticalInput * velocity * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);
        //Modifica los parámetros del animator en función de la circunstancia
        if (verticalInput > 0 && isOnGround)
        {
            //Se mueve hacia delante
            animator.SetBool("isMovingForward", true);
            animator.SetBool("isMovingBackward", false);
            animator.SetBool("isRotating", false);
        }
        else if (verticalInput < 0 && isOnGround)
        {
            //Se mueve hacia detrás
            animator.SetBool("isMovingBackward", true);
            animator.SetBool("isMovingForward", false);
            animator.SetBool("isRotating", false);
        }
        else 
        {
            //No se mueve
            animator.SetBool("isMovingForward", false);
            animator.SetBool("isMovingBackward", false);
        }
    }

    private void rotate(float horizontalInput) 
    {
        //Rota al personaje y lo anima
        //Recoge el input de A y D y genera un quaternion
        float turn = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        //En función de si está retrocediendo o no, gira normal o inverso
        if (verticalInput >= 0)
        {
            rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
        }
        else if (verticalInput < 0)
        {
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Inverse(turnRotation));
        }      
        //Modifica los parámetros del animator
        if (horizontalInput != 0 && verticalInput == 0)
        {
            animator.SetBool("isRotating", true);
        }
        else 
        {
            animator.SetBool("isRotating", false);
        }
    }

    private void modifyScale() 
    {
        //Disminuye la escala del personaje y los parámetros de salto y velocidad
        if (Input.GetKey(KeyCode.Q) && transform.localScale.y > smallLimit)
        {
            transform.localScale = transform.localScale - new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            velocity -= Time.deltaTime * parameterModifier;
            jumpForce -= Time.deltaTime * parameterModifier;
        }
        //Aumenta la escala del personaje y los parámetros de salto y velocidad
        if (Input.GetKey(KeyCode.E) && transform.localScale.y < bigLimit)
        {
            transform.localScale = transform.localScale + new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            velocity += Time.deltaTime * parameterModifier;
            jumpForce += Time.deltaTime * parameterModifier;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Modifica parámetros de control cuando el personaje colisiona con algo
        //Esto se debería modificar para que sólo se active cuando se choca por abajo con algo
        isOnGround = true;
        animator.SetBool("isOnGround", true);
    }
}
