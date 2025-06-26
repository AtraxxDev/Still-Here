// ====================================
// PLAYER INTERACTION (M�dulo de Interacci�n)
// ====================================
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuraci�n de Interacci�n")]
    public float interactionRange = 3f;
    public LayerMask interactableLayer = -1; // Todas las capas por defecto

    private Transform raycastOrigin;
    private IInteractable currentInteractable;

    public void Initialize(Transform cameraTransform)
    {
        raycastOrigin = cameraTransform;
    }

    public void CheckForInteractables()
    {
        if (raycastOrigin == null) return;

        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, interactionRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable = interactable;
                    OnInteractableFound(interactable);
                }
            }
            else
            {
                ClearCurrentInteractable();
            }
        }
        else
        {
            ClearCurrentInteractable();
        }
    }

    public void TryInteract()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void OnInteractableFound(IInteractable interactable)
    {
        // Aqu� puedes agregar l�gica para mostrar UI, etc.
        Debug.Log($"Objeto interactuable encontrado: {interactable.GetInteractionText()}");
    }

    void ClearCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
            Debug.Log("No hay objetos interactuables");
        }
    }

    // M�todos p�blicos para obtener informaci�n
    public bool HasInteractable() { return currentInteractable != null; }
    public string GetCurrentInteractionText() { return currentInteractable?.GetInteractionText() ?? ""; }
}