using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "ProjectCore/Variables/Int")]
    [InlineEditor(Expanded = false)]
    public class Int : ScriptableObject
    {
        [SerializeField] protected int Value;
        [SerializeField] protected int DefaultValue;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;

        private void OnEnable()
        {
            if (ResetToDefaultOnPlay)
            {
                Value = DefaultValue;
            }
        }

        public virtual int GetValue()
        {
            return Value;
        }

        public virtual void SetValue(int value)
        {
            Value = value;
        }

        public virtual void SetValue(Int value)
        {
            Value = value.Value;
        }

        public virtual void ApplyChange(int amount)
        {
            Value += amount;
        }

        public virtual void ApplyChange(Int amount)
        {
            Value += amount.Value;
        }

        public static implicit operator int(Int integer)
        {
            return integer.Value;
        }
    }
}
