using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagItem : MonoBehaviour
{
    private ItemObject _item;
    private void OnEnable()
    {
        _item =  List_ItemObject.Instation.RandomItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            Inventory.instation.AddItem(_item);
            gameObject.SetActive(false);
        }
    }
}
