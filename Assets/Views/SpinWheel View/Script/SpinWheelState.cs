using ProjectCore.Events;
using ProjectCore.StateMachine;
using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SpinWheelState", menuName = "ProjectCore/State Machine/States/SpinWheel State")]
public class SpinWheelState : State
{
    [SerializeField] private string PrefabName;

    [SerializeField] private GameEvent GotoMainMenu;
    [NonSerialized] private SpinWheelView _spinWheelView;
    public override IEnumerator Execute()
    {
        base.Execute();
        _spinWheelView = Instantiate(Resources.Load<SpinWheelView>(PrefabName));

        yield return _spinWheelView.Show(true);
    }

    public override IEnumerator Exit()
    {
        yield return _spinWheelView.Hide(true);
        _spinWheelView = null;
        yield return base.Exit();
    }

    public void GotoMainMene()
    {
        GotoMainMenu.Invoke();
    }
}
