using ProjectCore.StateMachine;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SpinWheelTransition", menuName = "ProjectCore/State Machine/Transitions/SpinWheel Transition")]
public class SpinWheelTransition : Transition
{
    public override IEnumerator Execute()
    {
        yield return base.Execute();

        UIViewState state = ToState as UIViewState;
    }
}
