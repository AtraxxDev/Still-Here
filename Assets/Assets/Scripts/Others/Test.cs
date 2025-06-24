using Sirenix.OdinInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameEventSO onColorChangeEvent;

    [Button]
    public void StartFunction()
    {
        onColorChangeEvent.TriggerEvent();
    }
}
