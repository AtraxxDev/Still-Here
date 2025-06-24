using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

// Script que se suscribe a un GameEventSO y ejecuta una o más acciones cuando el evento es disparado.
// Este script debe colocarse en los objetos que deben reaccionar a eventos usando UnityEvents.
public class GameEventListener : MonoBehaviour
{
    [Title("ScriptableEvent")]
    [InfoBox("Set the ScriptableEvent you want to trigger.")]
    public GameEventSO gameEvent;

    [InfoBox("Set the functions you want the game event to do.")]
    public UnityEvent onEventTriggered;

    /// <summary>
    /// Para declarar un evento desde el o los script que quieran lanzar el evento para que se ejecute se declara asi:
    /// public GameEventSO onPlayerDeath
    /// 
    /// onPlayerDeath.TriggerEvent();
    /// <summary>

    private void OnEnable()
    {
        gameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        gameEvent.RemoveListener(this);
    }

    // Ejecuta todas las acciones de los suscriptores.
    public void OnEventTriggered()
    {
        onEventTriggered?.Invoke();
    }
}
