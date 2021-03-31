using UnityEngine;

public class StateMachineEnemy : StateMachineBehaviour
{
    
    private bool _action;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var Enemy = animator.GetComponent<EnemySystem>();
        _action = true;
       Enemy.isHitAnimator(_action);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _action = false;
       
    }

   

    
}
