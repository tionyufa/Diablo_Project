using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class Player : MonoBehaviour
{
    public static Player singleton;
    [Header("ScriptesPlayer")]
    [SerializeField] private InputPlayer _inputPlayer;
    [SerializeField] private AttackPlayer _attackPlayer;
    [SerializeField] private PlayerCharacterics _playerCharacterics;
    
    [Header("Move")]
    [SerializeField] private LayerMask _layer;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    private bool _playeAction,isEnemy,notActive;
    private Ray _ray;
    private RaycastHit _hit;
    private Vector3 _point;
   
    [Header("Attack")]
    private bool attack1, attack2, skill1, skill2, skill3;
    private GameObject _target;
    public UnityEvent _eventSkill_1,_eventSkill_2,_eventSkill_3;
    private void Awake()
    {
        singleton = this;
        _point = transform.position;
        _target = gameObject;
    }

    private void Start()
    {
        AddSkill_1(Attack_1);
        AddSkill_2(Attack_2);
        AddSkill_3(UpDamage);
    }


    private void FixedUpdate()
    {
       MovePlayer();
       Check();
    }

    private void UpDamage()
    {
        StartCoroutine(StartActiveSkill());
    }
    
    IEnumerator StartActiveSkill()
    {
        _animator.SetTrigger(nameAnimator.HashActive);
        _rigidbody.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        _rigidbody.isKinematic = false;
       
    }
   
    private void MovePlayer()
    {
        if (_inputPlayer.GetMove() && !_playeAction)
        {
            _rigidbody.isKinematic = false;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100, _layer))
            { 
                if (_hit.collider.GetComponent<EnemyHealts>() != null)
                {
                    isEnemy = true;
                    _target = _hit.collider.gameObject;
                    _animator.SetFloat(nameAnimator.HashRun, 1f);
                    return;
                }
            }
            isEnemy = false;
            _point = _hit.point;
            _animator.SetFloat(nameAnimator.HashRun, 1f);

        }
        
    }

   

    private void Check()
    {
        Vector3 position = transform.position;
        if (isEnemy )
        {
            var targetPosition = _target.transform.position;
            _agent.SetDestination(targetPosition);
            transform.LookAt(targetPosition);
            float x = Mathf.Abs(position.x - targetPosition.x);
            float z = Mathf.Abs(position.z - targetPosition.z);

            if (x <= 5f && z <= 5f && _inputPlayer.isAttack_1())
            {
                 attack1 = true;
                 _agent.SetDestination(targetPosition);
                _animator.SetFloat(nameAnimator.HashRun, 0f);
                _eventSkill_1.Invoke();
            } 
            else if (x <= 5f && z <= 5f && _inputPlayer.isAttack_2())
            {
                 attack1 = false; 
                _agent.SetDestination(targetPosition);
                _animator.SetFloat(nameAnimator.HashRun, 0f);
                _eventSkill_2.Invoke();
            }
        }
        else if (!isEnemy)
        {
            var targetPosition = _point;
            _agent.SetDestination(targetPosition);
            float x = Mathf.Abs(position.x - targetPosition.x);
            float z = Mathf.Abs(position.z - targetPosition.z);

            if (x <= 2f && z <= 2f)
            {
                _agent.SetDestination(position);
                _animator.SetFloat(nameAnimator.HashRun, 0f);

            }
        }

    }
    
    public void Attack_1()
    {
        if (!_playeAction && attack1)
        {
            _rigidbody.isKinematic = true;
            _attackPlayer.SetTarget(_target);
            _animator.SetTrigger(nameAnimator.HashAttack);
            _inputPlayer.returnState();
            _target = gameObject;
        }
    }
    public void Attack_2()
    {
        if (!_playeAction && !attack1)
        { 
            _rigidbody.isKinematic = true;
            _animator.SetTrigger(nameAnimator.HashAttackPassive);
            _inputPlayer.returnState();
            _target = gameObject;
        }
    }
    

    public void AddSkill_1(UnityAction _action)
    {
        _eventSkill_1.AddListener(_action);
    }
    public void AddSkill_2(UnityAction _action)
    {
        _eventSkill_2.AddListener(_action);
    }
    public void AddSkill_3(UnityAction _action)
    {
        _eventSkill_3.AddListener(_action);
    }
    
    public void SetBonus()
    {
        PlayerCharacterics.singleton.tempBonus(Stat.Damage,5,15);
    }
    

    public void ActionAnimator(bool action)
    {
        _playeAction = action;
    }
    
}
