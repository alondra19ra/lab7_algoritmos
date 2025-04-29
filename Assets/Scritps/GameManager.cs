using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MyHashTable<string, GameObject> objectTable;

    [SerializeField] private GameObject[] gameObjectsToRegister;

    void Start()
    {
        
        objectTable = new MyHashTable<string, GameObject>(10);

        
        foreach (var obj in gameObjectsToRegister)
        {
            if (obj != null)
            {
                objectTable.Add(obj.name, obj);
                Debug.Log("Registrado: " + obj.name);
            }
        }

        string key = "Player"; 
        GameObject found = objectTable.Get(key);

        if (found != null)
        {
            Debug.Log("Objeto encontrado con clave '" + key + "': " + found.name);
        }

        objectTable.Remove(key);

        bool exists = objectTable.ConstainsKey(key);
        Debug.Log("¿'" + key + "' aún existe en la tabla? " + exists);
    }
}

