using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class NameString
{
    public readonly static string UPDAMAGE = "Skill1";
    public readonly static string FATALSKILL = "Skill2";
}
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
    private Dictionary<string, float> _cdSpeels = new Dictionary<string, float>();

    [Header("Attack")] 
    [SerializeField] private Image _imageSpellActive;
    private bool attack1, attack2, skill1, skill2, skill3;
    private GameObject _target;
    [HideInInspector] public UnityEvent _eventSkill_1,_eventSkill_2,_eventSkill_3;
    private void Awake()
    {
        
        singleton = this;
        _target = gameObject;
        _cdSpeels.Add(NameString.UPDAMAGE,0f); _cdSpeels.Add(NameString.FATALSKILL,0f);
        _imageSpellActive.fillAmount = 1f;
    }

    private void Start()
    {
        AddSkill_1(Attack_1);
        AddSkill_2(Attack_2);
        AddSkill_3(UpDamage);
        Load();
    }

    

    private void FixedUpdate()
    {
       MovePlayer();
       Check();
    }

    IEnumerator UpdatecdSpeels(string _name)
    {
        while (_cdSpeels[_name] > 0)
        {
            _cdSpeels[_name] -= 1f;
            _imageSpellActive.fillAmount = 1 - (_cdSpeels[_name] / 30);
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpDamage()
    {
        if (_cdSpeels[NameString.UPDAMAGE] <= 0)
        {
            StartCoroutine(StartActiveSkill());
        }
    }
    
    IEnumerator StartActiveSkill()
    {
        _animator.SetTrigger(nameAnimator.HashActive);
        _rigidbody.isKinematic = true;
        yield return new WaitForSeconds(1.5f);
        _rigidbody.isKinematic = false;
        _cdSpeels[NameString.UPDAMAGE] = 30;
        StartCoroutine(UpdatecdSpeels(NameString.UPDAMAGE));

    }

   
   
    private void MovePlayer()
    {
        if (_inputPlayer.GetMove() && !_playeAction)
        {
            _rigidbody.isKinematic = false;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100, _layer))
            {
                if (_hit.collider.gameObject.layer == 10)
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
                 _agent.SetDestination(position);
                _animator.SetFloat(nameAnimator.HashRun, 0f);
                _eventSkill_1.Invoke();
            } 
            else if (x <= 5f && z <= 5f && _inputPlayer.isAttack_2())
            {
                 attack1 = false; 
                _agent.SetDestination(position);
                _animator.SetFloat(nameAnimator.HashRun, 0f);
                _eventSkill_2.Invoke();
            }

            else if (x <= 5f && z <= 5f)
            {
                _agent.SetDestination(position);
                _animator.SetFloat(nameAnimator.HashRun, 0f);
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
            _playerCharacterics.GetEnergy(2); //временно

        }
    }
    public void Attack_2()
    {
        if (!_playeAction && !attack1 && _playerCharacterics.GetEnergy(0) >= 15)
        { 
            _rigidbody.isKinematic = true;
            _animator.SetTrigger(nameAnimator.HashAttackPassive);
            _inputPlayer.returnState();
            
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
        PlayerCharacterics.singleton.tempBonus(CharacterType.Damage,5,15);
    }
    

    public void ActionAnimator(bool action)
    {
        _playeAction = action;
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + "MySave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "MySave.dat", FileMode.Open);
            SaveDate saveDate = (SaveDate) bf.Deserialize(file);
            file.Close();
            gameObject.transform.position = new Vector3(saveDate.X,saveDate.Y,saveDate.Z);
            
            Debug.Log(saveDate.time);
        }
    }
    
}
