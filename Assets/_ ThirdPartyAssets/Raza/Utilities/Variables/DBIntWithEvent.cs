using System;
using System.Collections;
using System.Collections.Generic;
using ProjectCore.Events;
using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "ProjectCore/Variables/Int Persistent With Event")]
    public class DBIntWithEvent : DBInt
    {
        [SerializeField] protected GameEvent ValueChanged;
        
        public override void ApplyChange(int amount)
        {
            base.ApplyChange(amount);
            ValueChanged.Invoke();
        }

        public override void ApplyChange(Int amount)
        {
            base.ApplyChange(amount);
            ValueChanged.Invoke();
        }

        public override void SetValue(int value)
        {
            base.SetValue(value);
            ValueChanged.Invoke();
        }

        public override void SetValue(Int value)
        {
            base.SetValue(value);
            ValueChanged.Invoke();
        }

        public void AddListener(GameEventHandler callback)
        {
            ValueChanged.Handler+=callback;
        }
        
        public void RemoveListener(GameEventHandler callback)
        {
            ValueChanged.Handler -= callback;
        }
    }
}