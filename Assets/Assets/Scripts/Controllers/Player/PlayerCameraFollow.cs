// ====================================
// PLAYER CAMERA FOLLOW (Seguimiento de C�mara) - VERSI�N CORREGIDA
// ====================================
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("Configuraci�n de C�mara")]
    public Transform cameraTarget;
    public float standingHeadOffset = 1.6f; // Altura de la cabeza cuando est� de pie
    public float crouchingHeadOffset = 1.0f; // Altura de la cabeza cuando est� agachado
    public float smoothSpeed = 5f;
    public float cameraTransitionSpeed = 8f; // Velocidad de transici�n de altura de c�mara

    private Transform cameraTransform;
    private PlayerMovement playerMovement; // Referencia para saber si est� agachado
    private bool isInitialized = false;
    private float currentHeadOffset;

    public void Initialize(Transform camera)
    {
        cameraTransform = camera;

        // Obtener referencia al PlayerMovement para detectar si est� agachado
        if (cameraTarget != null)
        {
            playerMovement = cameraTarget.GetComponent<PlayerMovement>();
        }

        currentHeadOffset = standingHeadOffset;
        isInitialized = true;

        // Posici�n inicial inmediata
        if (cameraTransform != null && cameraTarget != null)
        {
            Vector3 headPosition = cameraTarget.position + Vector3.up * currentHeadOffset;
            cameraTransform.position = headPosition;
            cameraTransform.rotation = cameraTarget.rotation;
        }
    }

    void LateUpdate()
    {
        if (!isInitialized || cameraTransform == null || cameraTarget == null) return;

        // Determinar la altura objetivo de la c�mara seg�n si est� agachado
        float targetHeadOffset = standingHeadOffset;
        if (playerMovement != null && playerMovement.IsCrouching())
        {
            targetHeadOffset = crouchingHeadOffset;
        }

        // Transici�n suave de la altura de la c�mara
        currentHeadOffset = Mathf.Lerp(currentHeadOffset, targetHeadOffset, cameraTransitionSpeed * Time.deltaTime);

        // Calcular posici�n deseada de la c�mara (a la altura de la cabeza)
        Vector3 desiredPosition = cameraTarget.position + Vector3.up * currentHeadOffset;

        // Seguimiento suavizado
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    public void SetStandingHeadOffset(float newOffset)
    {
        standingHeadOffset = newOffset;
    }

    public void SetCrouchingHeadOffset(float newOffset)
    {
        crouchingHeadOffset = newOffset;
    }
}