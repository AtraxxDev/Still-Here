// ====================================
// PLAYER CAMERA FOLLOW (Seguimiento de Cámara) - VERSIÓN CORREGIDA
// ====================================
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    [Header("Configuración de Cámara")]
    public Transform cameraTarget;
    public float standingHeadOffset = 1.6f; // Altura de la cabeza cuando está de pie
    public float crouchingHeadOffset = 1.0f; // Altura de la cabeza cuando está agachado
    public float smoothSpeed = 5f;
    public float cameraTransitionSpeed = 8f; // Velocidad de transición de altura de cámara

    private Transform cameraTransform;
    private PlayerMovement playerMovement; // Referencia para saber si está agachado
    private bool isInitialized = false;
    private float currentHeadOffset;

    public void Initialize(Transform camera)
    {
        cameraTransform = camera;

        // Obtener referencia al PlayerMovement para detectar si está agachado
        if (cameraTarget != null)
        {
            playerMovement = cameraTarget.GetComponent<PlayerMovement>();
        }

        currentHeadOffset = standingHeadOffset;
        isInitialized = true;

        // Posición inicial inmediata
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

        // Determinar la altura objetivo de la cámara según si está agachado
        float targetHeadOffset = standingHeadOffset;
        if (playerMovement != null && playerMovement.IsCrouching())
        {
            targetHeadOffset = crouchingHeadOffset;
        }

        // Transición suave de la altura de la cámara
        currentHeadOffset = Mathf.Lerp(currentHeadOffset, targetHeadOffset, cameraTransitionSpeed * Time.deltaTime);

        // Calcular posición deseada de la cámara (a la altura de la cabeza)
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