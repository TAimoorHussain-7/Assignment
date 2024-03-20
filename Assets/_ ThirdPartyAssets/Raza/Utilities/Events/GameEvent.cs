using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCore.Events
{
    public delegate void GameEventHandler();

    [CreateAssetMenu(fileName = "e_", menuName = "ProjectCore/Events/Game Event - Basic")]
    [InlineEditor(Expanded = false)]
    public class GameEvent : ScriptableObject
    {
        public event GameEventHandler Handler;

        [Button(ButtonSizes.Medium)]
        public void Invoke()
        {
            Handler?.Invoke();
        }
    }
}
