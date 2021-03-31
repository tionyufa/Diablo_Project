using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour
{
    private Vector3 p0, p1, p2, p3 , random;
    private readonly int _LAYERNAME = 9;
    [SerializeField] private float _money;
    private bool isTrigger,checkNew = true;

    private void Start()
    {
       
    } 

    private void Update()
    {
        tranform();
    }

    public void tranform()
    {
        if (isTrigger)
        {
            p3 = Player.singleton.transform.position + random ;
            
            transform.position = Bezier.GetBezier(p0, p1, p2, p3, _money);
            _money += Time.deltaTime ;
            if (_money >= 1f)
            {
                isTrigger = false;
                _money = 0;
                gameObject.SetActive(false);
                
            }
        }
    }

    public void check()
    {
        if (checkNew)
        {
            random = new Vector3(0f, Random.Range(2f, 3f), 0f);
            p0 = transform.position;
            p1 = transform.position + new Vector3(0, Random.Range(2, 5f), 0);
            p2 = Player.singleton.transform.position +
                 new Vector3(Random.Range(2, 5f), Random.Range(2, 5f), Random.Range(2, 5f));
            checkNew = false;
        }
    }

    private void OnDrawGizmos()
    {
        int f = 20;
        Vector3 prev = p0;

        for (int i = 0; i < f; i++)
        {
            float paremetr = (float) i / 20;
            Vector3 point = Bezier.GetBezier(p0, p1, p2, p3, paremetr);
            Gizmos.DrawLine(prev,point);
            prev = point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _LAYERNAME)
        {
            check();
            isTrigger = true;
           
        }
        
    }
}
