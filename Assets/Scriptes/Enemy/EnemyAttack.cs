using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
     [SerializeField] private Transform hand;
     [SerializeField] private float _damage;
     
    [Header("Melee")]
    [SerializeField] private float _radius;
    [SerializeField] private float _distation;
    [Header("Range")]
    [SerializeField] private List <GameObject> web;
    private int index,index2;
    
    public void MeleeHitEnemy()
    {
        Ray ray = new Ray(hand.position,hand.forward);
        RaycastHit [] _hits;
        _hits = Physics.SphereCastAll(ray, _radius,_distation);
     
        for (int i = 0; i < _hits.Length; i++)
        {
            Collider col = _hits[i].collider;
            if (col.GetComponent<PlayerCharacterics>() != null)
            {
                col.GetComponent<PlayerCharacterics>()._playerEvent.Invoke(_damage);
                
            }
        }
    }

    public void RangeHitEnemy()
    {
        Ray ray = new Ray(transform.position,transform.forward);
        for (int i = 0; i < web.Count; i++)
        {
            if (!web[index].activeSelf)
            {
                web[index].SetActive(true);
                web[index].transform.parent = null;
                web[index].GetComponent<ShellHit>().setSetting(ray.direction,_damage);
                index++;
                if (index > web.Count - 1) index = 0;
                Invoke("returnWeb",5f);
                return;
            }
        }
    }

   private void returnWeb ()
    {
        for (int i = 0; i < web.Count; i++)
        {
            if (web[index2].activeSelf)
            {
                web[index2].SetActive(false);
                web[index2].transform.SetParent(hand);
                web[index2].transform.localPosition = Vector3.zero;
                index2++;
                if (index2 > web.Count - 1) index2 = 0;
                return;
            }

            if (!web[index2].activeSelf)
            {
                web[index2].transform.SetParent(hand);
                web[index2].transform.localPosition = Vector3.zero;
                index2++;
                if (index2 > web.Count - 1) index2 = 0;
                return;
            }
        }
    }
 }
