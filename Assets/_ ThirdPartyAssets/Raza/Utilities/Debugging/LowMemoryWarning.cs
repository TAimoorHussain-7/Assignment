using ExtensionMethods;
using ProjectCore.Variables;
using UnityEngine;

public class LowMemoryWarning : MonoBehaviour
{
    [SerializeField] private CanvasGroup LowMemoryWarningCanvasGroup;
    [SerializeField] private BoolWithEvent MemoryDebuggerEnabled;
    
    private void OnEnable()
    {
        Application.lowMemory += ShowLowMemoryWarning;
    }

    private void OnDisable()
    {
        Application.lowMemory -= ShowLowMemoryWarning;
    }
    
    private void ShowLowMemoryWarning()
    {
        if (MemoryDebuggerEnabled)
        {
            LowMemoryWarningCanvasGroup.SetAlpha(1);

        }
    }
}
