using UnityEngine;

namespace CodeBase.Logic.Animator
{
  public class AnimatorStateReporter : StateMachineBehaviour
  {
    private IAnimationStateReader _stateReader;

    public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      FindReader(animator);
      base.OnStateEnter(animator, stateInfo, layerIndex);
      _stateReader.EnteredState(stateInfo.shortNameHash);
    }

    public override void OnStateExit(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      FindReader(animator);
      base.OnStateExit(animator, stateInfo, layerIndex);
      _stateReader.ExitedState(stateInfo.shortNameHash);
    }

    private void FindReader(UnityEngine.Animator animator)
    {
      if (_stateReader != null)
        return;
      _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
  }
}