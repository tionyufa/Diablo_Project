using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[System.Serializable]
public class Enemy
{
   public List <SkinnedMeshRenderer> _meshRenderers;
   public float maxHealts;
   [HideInInspector] public float currentHP;
   public string Name;
   public EnemyList _myTyp;

   public void setHP(float _hp)
   {
      currentHP = _hp;
   }

   public void AddOutline()
   {
      for (int i = 0; i < _meshRenderers.Count; i++)
      {
         _meshRenderers[i].material.color = Color.red;
      }
   }

   public void ShowOutline()
   {
      for (int i = 0; i < _meshRenderers.Count; i++)
      {
         _meshRenderers[i].material.color = Color.white;
      }
   }
}

[RequireComponent(typeof( NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyHealts))]
[RequireComponent(typeof(EnemyAttack))]
public abstract class EnemySystem : MonoBehaviour
{
   [SerializeField] private Enemy _myChar;
   [SerializeField] protected float _speed;
   protected NavMeshAgent _agent;
   protected Animator _animator;
   
   [Header("Patrol")] 
   protected LayerMask _playerLayer,_ground;
   [SerializeField] protected float _radiusCheck,_radiusAttack,_walkPointRange;
   protected  Vector3 patrolPoint;
   protected bool isCheckPlayer,isAttack,isMove,isAction;

   private void Start()
   {
      _myChar.currentHP = _myChar.maxHealts;
      _playerLayer = LayerMask.GetMask("Player");
      _ground = LayerMask.GetMask("Ground");
      _agent = GetComponent<NavMeshAgent>();
      _animator = GetComponent<Animator>();
      _agent.speed = _speed;
   }

   private void Update()
   {
      Check();
   }

   
   public  void Patrol()
   {
      if (!isMove) SearchWalk();
        
      if (isMove) _agent.SetDestination(patrolPoint);

      float x  = transform.position.x - patrolPoint.x;
      float z = transform.position.z - patrolPoint.z;
        

      if (Mathf.Abs(x) <= 1f && Mathf.Abs(z) <= 1f)
      {
         isMove = false;
      }
   }
   public void SearchWalk()
   {
        
      float randomX= Random.Range(-_walkPointRange, _walkPointRange);
      float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
      patrolPoint = new Vector3(transform.position.x + randomX,transform.position.y,transform.position.z + randomZ);

      if (Physics.Raycast(patrolPoint, -transform.up, 10f, _ground))
      {
         isMove = true;
           
      }
   }

   public void Check()
   {
      isCheckPlayer =  Physics.CheckSphere(transform.position, _radiusCheck, _playerLayer);
      isAttack = Physics.CheckSphere(transform.position, _radiusAttack, _playerLayer);
      
      if (!isCheckPlayer && !isAttack) Patrol();
      if (isCheckPlayer && !isAttack) ChosePlayer();
      if (isCheckPlayer && isAttack) Attack();
   }

   public  void ChosePlayer()
   {
      _agent.SetDestination(Player.singleton.transform.position);
   }
   
   public abstract void Attack();

   public Enemy getChar()
   {
      return _myChar;
   }

   private void OnMouseEnter()
   {
      ToolTip.singleton.ToolTipAction(_myChar);
   }

   private void OnMouseExit()
   {
      ToolTip.singleton.ToolTipExit();
   }
}

public enum EnemyList
{
   Demon,
   Skelet,
   Animal
   
}


