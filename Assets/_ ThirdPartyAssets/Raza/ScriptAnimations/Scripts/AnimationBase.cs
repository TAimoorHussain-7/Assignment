using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCore.ScriptAnimations
{
    [CreateAssetMenu(fileName = "AnimationBase", menuName = "ProjectCore/ScripAnimations/AnimationBase")]
    public class AnimationBase : ScriptableObject
    {
        public virtual IEnumerator Execute()
        {
            yield break;
        }
    }
}
