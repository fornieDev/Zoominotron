using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

//Clase para gestionar el menú principal
public class MainMenuManager : MonoBehaviour
{
    //Variables
    private List<SaveData> saveDataGlobal = new List<SaveData>();
    [SerializeField] private GameObject[] loadButtons;
    private DataPersist dataPersistScript;
    private SceneTransitionManager sceneTransitionManagerScript;
    private GameObject mainButtons;
    [SerializeField] private GameObject loadGames;

    private void Start()
    {
        //inicialización
        sceneTransitionManagerScript = GameObject.Find("SceneTransitionBlack").GetComponent<SceneTransitionManager>();
        mainButtons = GameObject.Find("MainButtons");
        dataPersistScript = GameObject.Find("DataPersist").GetComponent<DataPersist>();
    }
    public void StartNewGame() 
    {
        //Carga la siguiente escena (cinemática)
        StartCoroutine(sceneTransitionManagerScript.LoadScene());
    }

    public void LoadGame()
    {
        //Muestra las partidas guardadas
        mainButtons.SetActive(false);
        loadGames.SetActive(true);
        loadDataFile();
    }

    public void ExitGame()
    {
        //Sale del juego
        //Sale del juego cuando se está ejecutando la versión del ejecutable
        Application.Quit();
        //Sale del juego cuando se está ejecutando desde el IDE
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private void loadDataFile()
    {
        //Carga las partidas del json y las muestras en los slots del menú de cargado
        string filePath = Application.persistentDataPath + "/saveFile.json";
        
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            //Guarda los datos en una variable
            SaveDataList saveDataList = JsonUtility.FromJson<SaveDataList>(json);
            saveDataGlobal = saveDataList.saveDataList;



            for (int i = 0; i < 3; i++)
            {
                if (saveDataGlobal[i].mapIndex != 0)
                {
                    loadButtons[i].GetComponent<TextMeshProUGUI>().text = "Mapa: " + Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(saveDataGlobal[i].mapIndex)) + ".Fecha: " + saveDataGlobal[i].dateTime.ToString();
                }
            }
        }
        else
        {
            //No se ha encontrado el archivo
        }
    }

    public void loadGameOnSlot(int slot)
    {
        //Carga la partida del slot correspondiente
        //Guarda los datos
        dataPersistScript.mapIndex = saveDataGlobal[slot].mapIndex;
        dataPersistScript.playerPosition = saveDataGlobal[slot].playerPosition;
        dataPersistScript.playerRotation = saveDataGlobal[slot].playerRotation;
        dataPersistScript.playerScale = saveDataGlobal[slot].playerScale;
        dataPersistScript.jumpForce = saveDataGlobal[slot].jumpForce;
        dataPersistScript.velocity = saveDataGlobal[slot].velocity;
        //Carga la escena
        StartCoroutine(sceneTransitionManagerScript.LoadSceneIndex(saveDataGlobal[slot].mapIndex));
    }

    public void closeLoadGames() 
    {
        //Deja de mostrar las partidas y vuelve al menú
        mainButtons.SetActive(true);
        loadGames.SetActive(false);
    }
}
