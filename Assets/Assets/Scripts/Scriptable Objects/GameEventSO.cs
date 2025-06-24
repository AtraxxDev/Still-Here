using System.Collections.Generic;
using UnityEngine;


// Creador de ScriptableEvents
[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Events/GameEvent")]
public class GameEventSO : ScriptableObject
{
    // Lista de los suscriptores para notificar cuando se lance el evento
    List<GameEventListener> listeners = new List<GameEventListener> ();

    // Funcion que lanza el evento de dicho scriptable event
    public void TriggerEvent()
    {
        // Bucle for al reves empezando del ultimo hasta llegar a 0 esto para evitar errores si alguien se remueve de la lista mientras se está iterando
        for (int i = listeners.Count - 1; i >= 0; i--) 
        {
            listeners [i].OnEventTriggered();
        }
    }

    public void AddListener(GameEventListener listener) => listeners.Add (listener);
    public void RemoveListener(GameEventListener listener) => listeners.Remove (listener);

}
