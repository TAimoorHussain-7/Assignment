using System;
using System.Globalization;
using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "ProjectCore/Variables/Bool Persistent")]
    public class DBBool : Bool, IDBVariable
    {
        [SerializeField] protected string Key;

        private void OnEnable()
        {
            Load();
        }

        public override void SetValue(bool value)
        {
            base.SetValue(value);
            Save();
        }

        public override void SetValue(Bool value)
        {
            base.SetValue(value);
            Save();
        }

        protected void Save()
        {
            DBManager.SetBool(this, Key, Value);
        }

        protected void Load()
        {
            if (!string.IsNullOrEmpty(Key) && DBManager.HasKey(this, Key))
            {
                Value = DBManager.GetBool(this, Key);
            }
            else
            {
                if (ResetToDefaultOnPlay)
                {
                    Value = DefaultValue;
                }
                else
                {
                    Value = false;
                }
            }
        }

        void IDBVariable.Update(object value)
        {
            bool boolean = Value;
            if (value is int)
            {
                boolean = Convert.ToInt32(value, CultureInfo.InvariantCulture) == 1;
            }
            else if (value is long)
            {
                boolean = Convert.ToInt32(value, CultureInfo.InvariantCulture) == 1;
            }
            else if (value is bool)
            {
                boolean = Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            }

            SetValue(boolean);
        }

        object IDBVariable.GetValue()
        {
            return GetValue() ? 1 : 0;
        }
    }
}
