using System.Collections.Generic;
using TGDebugColors;
using UnityEngine;
using UnityEngine.UI;

public class UICollectibleList : MonoBehaviour
{
    // Lista de entradas de los ID y Imagenes que representan cada item de la lista
   [SerializeField] private List<CollectibleEntry> entries;

    // Funcion que recibe el ID del item recogido y lo iguala a las entries para prender su imagen
   public void MarkAsCollected(string collectibleID)
    {
        foreach(var entry in entries)
        {
            if (entry.id == collectibleID)
            {
                DebugColors.printColor($"Si se encontro: {collectibleID} en la lista y se marco",DebugColors.MAGENTA);
                entry.markChecked.enabled = true;
                break;
            }
        }
    }
}

[System.Serializable]
public class CollectibleEntry
{
    public string id;
    public Image markChecked;
}
