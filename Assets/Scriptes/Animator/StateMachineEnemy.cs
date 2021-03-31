using UnityEngine;

public class StateMachineEnemy : StateMachineBehaviour
{
    
    private bool _action;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var Enemy_Golem = animator.GetComponent<EnemyGolem>();
        _action = true;
       Enemy_Golem.isHitAnimator(_action);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _action = false;
       
    }

   

    
}
