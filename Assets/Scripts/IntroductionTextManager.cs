using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;

//Clase que gestiona la narración de la cinemática
public class IntroductionTextManager : MonoBehaviour
{
    //Componentes
    private TextMeshProUGUI introductionText;
    //Variables externas
    [SerializeField] public int index;
    //Variables internas
    private string[] introductionTexts = new string[] {
        "Un día como otro cualquiera,Nikola,un prodigio de la ciencia,hacía experimentos en el laboratorio de su habitación.",
        "Su última investigación,consistía en un rayo capaz de contraer y expandir la materia...el Zoominoutron.",
        "No se sabe qué salió mal,pero por alguna razón,sufrió una sobrecarga,disparando accidentalmente.",
        "Como de costumbre la puerta estaba cerrada con llave,por lo que al no poder detener el rayo...",
        "...al contraerse tanto la materia de la llave,ésta colapsó,desintegrándose y quedando atrapado.",
        "",
        "Por si todo esto fuese poco,la descontrolada liberación de zoomitrones,ha desetabilziado sus átomos...",
        "...por lo que parece que ahora él mismo puede contraerse y expandirse a voluntad.",
        "Sin embargo,este inesperado resultado podría volverse inestable,por lo que podría colapsar él también.",
        "Por suerte,su madre,la doctora Salas,es experta en el campo de la investigación de partículas zoomitrónicas.",
        "Debería buscarla para encontrar la fórmula para estabilizarse antes de que ocurra una catástrofe...",
        ""
    };
    private bool rayHasFired;
    //GameObjects
    [SerializeField] private GameObject rayShort;
    [SerializeField] private GameObject rayLong;
    

    void Start()
    {
        //inicialización
        introductionText = GetComponent<TextMeshProUGUI>();
        index = 0;
        rayHasFired = false;
        //Corrutina que llama repetidamente al método showtext
        InvokeRepeating("showText",1f,6f);
    }

    private void showText() 
    {
        //Cambia el texto de la cinemática y aumenta el index
        introductionText.text = introductionTexts[index];
        if (index < introductionTexts.Length - 1) { index++; }
        //Aquí se activa la animación del rayo cuando la narración llega a un punto concreto
        if (index == 2 && !rayHasFired) 
        {
            if (rayShort != null) { rayShort.SetActive(true); }
            StartCoroutine(TurnOffRay());
            rayHasFired=true;
        }
        if (index == 4) 
        {
            if (rayLong != null) { rayLong.SetActive(true); }
        }
    }

    IEnumerator TurnOffRay() 
    {
        //Corrutina para apagar el rayo con delay
        yield return new WaitForSeconds(0.3f);
        if (rayShort != null) { rayShort.SetActive(false); }
    }
}
