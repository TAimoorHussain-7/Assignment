using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ExtensionMethods
{
    public static class TextMeshProExtensions
    {
        public static void SetOpacity(this TextMeshProUGUI textMesh, float alpha)
        {
            var currentColor = textMesh.color;
            currentColor.a = alpha;
            textMesh.color = currentColor;
        }
    }
}
