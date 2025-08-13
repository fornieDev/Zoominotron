using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

//Dataclass para serializar los datos y convertirlos a json
[System.Serializable]
class SaveData 
{
    //Datos
    public int mapIndex;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 playerScale;
    public float velocity;
    public float jumpForce;
    public string dateTime;
}

//Clase envolvente para facilitar el tratamiento de los datos en lista
[System.Serializable]
class SaveDataList
{
    public List<SaveData> saveDataList;
}
