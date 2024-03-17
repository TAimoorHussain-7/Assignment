using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ProjectCore.Variables
{
    public abstract class Array<T> : ScriptableObject
    {
        [Searchable]
        public List<T> list;
        [SerializeField] protected bool ResetToDefaultOnPlay = true;

        private void OnEnable()
        {
            if (ResetToDefaultOnPlay)
            {
                Clear();
            }
        }

        public int Count
        {
            get
            {
                return list.Count;
            }
        }

        public void Clear()
        {
            if (list != null)
            {
                list.Clear();
            }
        }

        public bool Add(T t)
        {
            if (list.Contains(t))
            {
                return false;
            }
            else
            {
                list.Add(t);
                return true;
            }
        }

        public void InsertAtIndex(int index, T t)
        {
            list.Insert(index, t);
        }

        public bool Remove(T t)
        {
            return list.Remove(t);
        }

        public T this[int index]
        {
            get
            {
                return list[index];
            }
        }
    }
}
