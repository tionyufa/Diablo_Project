using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SaveDate
{
    public float X,Y,Z;
    public float time;
}
public class SavePoint : MonoBehaviour
{
    [SerializeField] private int LAYERPLAYER;
    private void Save(Transform _transform)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "MySave.dat");
        SaveDate saveDate = new SaveDate();
        saveDate.X = _transform.position.x;
        saveDate.Y = _transform.position.y;
        saveDate.Z = _transform.position.z;
        saveDate.time = Time.time;
        bf.Serialize(file,saveDate);
        file.Close();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LAYERPLAYER)
        {
            Save(other.transform);
        }
    }
}
