using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "ProjectCore/Variables/String")]
    public class String : ScriptableObject
    {
        [SerializeField] protected string Value;
        [SerializeField] protected string DefaultValue;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;

        private void OnEnable()
        {
            if (ResetToDefaultOnPlay)
            {
                Value = DefaultValue;
            }
        }

        public string GetValue()
        {
            return Value;
        }

        public virtual void SetValue(string value)
        {
            Value = value;
        }

        public virtual void SetValue(String value)
        {
            Value = value.Value;
        }

        public static implicit operator string(String str)
        {
            return str.Value;
        }
    }
}
