// ====================================
// EJEMPLO DE OBJETO INTERACTUABLE
// ====================================
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [Header("Configuraci�n")]
    public string interactionText = "Presiona E para interactuar";

    public void Interact()
    {
        Debug.Log($"Interactuando con {gameObject.name}");
        // Aqu� va tu l�gica espec�fica
    }

    public string GetInteractionText()
    {
        return interactionText;
    }
}