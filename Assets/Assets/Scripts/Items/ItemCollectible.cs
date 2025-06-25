using Sirenix.OdinInspector;
using UnityEngine;
using TGDebugColors;
public class ItemCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private string collectibleID;

    // Obtiene el id del item para poder ser llamado
    public string GetCollectibleID() => collectibleID;

    // Funcion que se hara al recolectar el objeto
    [Button]
    public void OnCollect(/*PlayerController player*/)
    {
        // funcion para agarrar el id del item desde el player
        //player.OnCollectibleCollected(this);

        DebugColors.printSuccess($"Recogí con exito el objeto {collectibleID}");

        //AudioManager.Instance.PlaySFX("");
        gameObject.SetActive(false);
    }

}
