using UnityEngine;

public interface ICollectible
{
    string GetCollectibleID();
    void OnCollect(/*PlayerController player*/);
}
