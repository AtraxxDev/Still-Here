// ====================================
// EJEMPLO DE OBJETO INTERACTUABLE
// ====================================
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    public string interactionText = "Presiona E para interactuar";

    public void Interact()
    {
        Debug.Log($"Interactuando con {gameObject.name}");
        // Aquí va tu lógica específica
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}