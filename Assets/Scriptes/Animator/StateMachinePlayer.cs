using UnityEngine;

public class StateMachinePlayer : StateMachineBehaviour
{
    private bool _action;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _action = true;
        InputPlayer.Instance.StateAction(_action);
        Player.singleton.ActionAnimator(_action);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _action = false;
        InputPlayer.Instance.StateAction(_action);
        Player.singleton.ActionAnimator(_action);
    }

   

    
}
