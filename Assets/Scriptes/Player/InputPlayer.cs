using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class InputPlayer : MonoBehaviour
{
    public static InputPlayer Instance { get; private set; }
    protected bool _isAttack,_isAttackPassive;
    protected bool _action, _active , _move;
    [SerializeField] private Player _player;
    private void Awake()
    {
        Instance = this;
    }

    public bool isAttack_1()
    {
        return _isAttack;
    }
    public bool isAttack_2()
    {
        return _isAttackPassive;
    }

    public void returnState()
    {
        _isAttack = false;
        _isAttackPassive = false;
    }

    
    private void Update()
    {
        CheckAttack();
        MovePlayer();
    }
   

    private void MovePlayer()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) _move = true;
       
        else
        {
            _move = false;
        }    

    }

    private void CheckAttack()
    {
        if (Input.GetMouseButtonDown(0) && !_action)
        {
            _isAttack = true;
            _isAttackPassive = false;
        }
        else if (Input.GetMouseButtonDown(1) && !_action)
        {
            _isAttackPassive = true;
            _isAttack = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && !_action)
        {
            _player._eventSkill_3.Invoke();
        }
    }

    public bool GetMove() {return _move;}
  

    public void StateAction(bool isAction)
    {
        _action = isAction;
       
    }

   
}



public class nameAnimator
{
    public static readonly int HashRun = Animator.StringToHash("Run");
    public static readonly int HashAttack = Animator.StringToHash("Attack");
    public static readonly int HashAttackPassive = Animator.StringToHash("AttackPassive");
    public static readonly int Dead = Animator.StringToHash("Dead");
    public static readonly int HashActive = Animator.StringToHash("Active");
}
