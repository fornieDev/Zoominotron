using TMPro;
using UnityEngine;

//Clase que gestiona el texto del tutorial
public class TutorialText : MonoBehaviour
{
    //variables
    private TextMeshProUGUI textMeshPro;
    private string[] tutorialHints = new string[]
    {
        "Parece que tienes un nuevo poder, pulsa Q o E para disminuirte o agrandarte.",
        "Genial.Mira a tu alrededor para buscar una salida,usa A o D para girar.",
        "Ve hacia donde quieras usando W o S para moverte",
        "Si necesitas saltar,pulsa SPACE",
        ""
    };
    private int index = 0;
    private DataPersist dataPersistScript;


    void Start()
    {
        //inicialización
        textMeshPro = GetComponent<TextMeshProUGUI>();
        dataPersistScript = GameObject.Find("DataPersist").GetComponent<DataPersist>();
        //Cuando esta escena se inicia sin cargar una partida(es la primera vez),muestra el tutorial a través de una corrutina
        if (dataPersistScript.mapIndex == 0) { InvokeRepeating("showTutorial", 1, 8); }

    }

    private void showTutorial() 
    {
        //Método para mostrar el texto del tutorial y aumentar el index
        textMeshPro.text = tutorialHints[index];
        if (index < tutorialHints.Length - 1) 
        {
            index++;
        }
    }
}
