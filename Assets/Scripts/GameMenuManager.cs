using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;
using TMPro;
using System;

//Clasea que gestiona el menú de juego
public class GameMenuManager : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject[] loadButtons;
    [SerializeField] private GameObject[] saveButtons;
    [SerializeField] private GameObject saveGames;
    [SerializeField] private GameObject loadGames;
    private GameObject player;
    private bool gameIsPaused = false;
    private SceneTransitionManager sceneTransitionManagerScript;
    private List<SaveData> saveDataGlobal = new List<SaveData>();
    private DataPersist dataPersistScript;


    private void Start()
    {
        //inicialización
        sceneTransitionManagerScript = GameObject.Find("SceneTransitionBlack").GetComponent<SceneTransitionManager>();
        dataPersistScript = GameObject.Find("DataPersist").GetComponent<DataPersist>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //conmuta el menú al pulsar escape
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (!gameIsPaused) { pauseGame(); }
            else { resumeGame(); }
        } 
    }

    private void pauseGame() {
        //Pausa el juego y abre el menú
        gameIsPaused = !gameIsPaused;
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
        buttons.SetActive(true);
    }

    public void resumeGame() {
        //reanuda el juego y cierra el menú
        gameIsPaused = !gameIsPaused;
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        loadGames.SetActive(false);
        saveGames.SetActive(false);
    }

    public void exitGame() {
        //Carga el menú principal
        Time.timeScale = 1f;
        StartCoroutine(sceneTransitionManagerScript.LoadSceneIndex(0));
    }

    public void showSaveGame() {
        //Muestra las partidas guardadas para guardar
        buttons.SetActive(false);
        saveGames.SetActive(true);

        loadDataFile();
    }

    public void showLoadGame() {
        //Muestra las partidas guardadas para cargar
        buttons.SetActive(false);
        loadGames.SetActive(true);

        loadDataFile();
    }

    public void saveGameOnSlot(int slot) 
    {
        //Guarda los datos en el json en el slot correspondiente
        //Crea un savedata con los datos de la partida
        SaveData saveData = new SaveData();
        saveData.mapIndex = SceneManager.GetActiveScene().buildIndex;
        saveData.playerPosition = player.transform.position;
        saveData.playerRotation = player.transform.rotation;
        saveData.playerScale = player.transform.localScale;
        saveData.velocity = player.GetComponent<PlayerControllerInGame>().velocity;
        saveData.jumpForce = player.GetComponent<PlayerControllerInGame>().jumpForce;
        saveData.dateTime = DateTime.Now.ToString();

        //Busca el archivo si existe para crear uno nuevo o actualizar el existente
        string filePath = Application.persistentDataPath + "/saveFile.json";
        SaveDataList saveDataList;
        if (!File.Exists(filePath))
        {
            //crear un archivo nuevo json con los datos en el slot correspondiente
            saveDataList = new SaveDataList();
            saveDataList.saveDataList = new List<SaveData> { null, null, null };
            saveDataList.saveDataList[slot] = saveData;
            string newJson = JsonUtility.ToJson(saveDataList);
            File.WriteAllText(filePath, newJson);
            //Cierra el menú de guardado
            buttons.SetActive(true);
            saveGames.SetActive(false);
            return;
        }
        else 
        {
            //Actualiza el fichero json en el slot correspondiente
            //Crea un saveDataList con las partidas del json
            saveDataList = new SaveDataList();
            saveDataList.saveDataList = saveDataGlobal;
            //crea un nuevo objeto con los datos
            SaveData updatedSaveData = new SaveData();
            updatedSaveData.mapIndex = SceneManager.GetActiveScene().buildIndex;
            updatedSaveData.playerPosition = player.transform.position;
            updatedSaveData.playerRotation = player.transform.rotation;
            updatedSaveData.playerScale = player.transform.localScale;
            updatedSaveData.velocity = player.GetComponent<PlayerControllerInGame>().velocity;
            updatedSaveData.jumpForce = player.GetComponent<PlayerControllerInGame>().jumpForce;
            updatedSaveData.dateTime = DateTime.Now.ToString();
            //Sobreescribe el json con el nuevo objeto en el slot correspondiente
            saveDataList.saveDataList[slot] = updatedSaveData;
            //Los transforma a json y lo guarda
            string newJson = JsonUtility.ToJson(saveDataList);
            File.WriteAllText(filePath, newJson);
            //cambia la interfaz
            buttons.SetActive(true);
            saveGames.SetActive(false);
            return;
        }
    }

    

    private void loadDataFile() 
    {
        //Carga las partidas del json y las muestras en los slots del menú de guardado y cargado
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
                    saveButtons[i].GetComponent<TextMeshProUGUI>().text = "Mapa: " + Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(saveDataGlobal[i].mapIndex)) + ".Fecha: " + saveDataGlobal[i].dateTime.ToString();
                }
            }
        }
        else 
        {
            //No se ha encontrado el fichero
        }
    }

    public void loadGameOnSlot(int slot) 
    {
        //Carga la partida del slot correspondiente
        //Reanuda la ejecución del juego
        Time.timeScale = 1f;
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
}
