using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellHit : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _damage;
    private Ray _ray;
    private Vector3 _v;
    

    public void setSetting(Vector3 ray,float damage)
    {
        _v = ray;
        _damage = damage;
        
    }

    private void Update()
    {
        transform.Translate(_v * Time.deltaTime * _speed);
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacterics>() != null)
        {
            other.GetComponent<PlayerCharacterics>()._playerEvent.Invoke(_damage);
            gameObject.SetActive(false);
        }
    }
}
