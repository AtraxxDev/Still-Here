using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameEventSO onColorChangeEvent;

    public UICollectibleList uiList;

    [Button]
    public void StartFunction()
    {
        onColorChangeEvent.TriggerEvent();
    }

    [Button]
    // Funcion para obtener el ID del collectible que se recogio y buscar en la lista si está.
    public void OnCollectibleCollected(ICollectible collectible)
    {
        string id = collectible.GetCollectibleID();
        uiList.MarkAsCollected(id);
    }

}
