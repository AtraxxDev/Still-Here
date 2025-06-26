// ====================================
// PLAYER MOVEMENT (M�dulo de Movimiento) - VERSI�N CORREGIDA
// ====================================
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float gravity = -9.81f;

    [Header("Configuraci�n de Agacharse")]
    public float crouchHeight = 1f;
    public float crouchSpeed = 1.5f;
    public float crouchTransitionSpeed = 5f;

    [Header("Collider Sync")]
    public Transform visualMesh; // Arrastra el objeto con el mesh visual aqu�
    public float colliderYOffset = 0f;

    private CharacterController controller;
    private Vector3 velocity;
    private float originalHeight;
    private bool isCrouching = false;
    private float targetHeight;
    private float currentHeight;

    public void Initialize()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        currentHeight = originalHeight;
        targetHeight = originalHeight;

        // Inicializar velocidad vertical
        velocity = Vector3.zero;

        // Sincronizar posici�n inicial
        SyncColliderPosition();
    }

    public void SyncColliderPosition()
    {
        if (visualMesh != null)
        {
            // CORRECCI�N: El mesh visual debe estar SIEMPRE en la misma posici�n que el CharacterController
            // El CharacterController ya maneja su posici�n correctamente
            visualMesh.position = transform.position;
            visualMesh.rotation = transform.rotation;
        }
    }

    public void HandleMovement(float horizontal, float vertical, bool isRunning, bool isCrouchingInput)
    {
        // Manejar entrada para agacharse
        bool wasGrounded = controller.isGrounded;
        isCrouching = isCrouchingInput;
        targetHeight = isCrouching ? crouchHeight : originalHeight;

        // CORRECCI�N: Transici�n suave de altura SIN mover el transform
        if (Mathf.Abs(controller.height - targetHeight) > 0.01f)
        {
            controller.height = Mathf.Lerp(controller.height, targetHeight, crouchTransitionSpeed * Time.deltaTime);
            controller.center = new Vector3(0, controller.height / 2f, 0);
        }
        else
        {
            controller.height = targetHeight;
            controller.center = new Vector3(0, controller.height / 2f, 0);
        }

        // Calcular direcci�n de movimiento
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Determinar velocidad (considerar si est� agachado)
        float currentSpeed = isRunning && !isCrouching ? runSpeed :
                           isCrouching ? crouchSpeed : walkSpeed;

        // Aplicar movimiento horizontal
        controller.Move(move * currentSpeed * Time.deltaTime);

        // CORRECCI�N: Aplicar gravedad correctamente
        ApplyGravity();
    }

    void ApplyGravity()
    {
        // CORRECCI�N: Verificar si est� en el suelo y resetear velocidad vertical
        if (controller.isGrounded)
        {
            // Si est� en el suelo, mantener una peque�a fuerza hacia abajo
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Peque�a fuerza para mantener contacto con el suelo
            }
        }
        else
        {
            // Solo aplicar gravedad si no est� en el suelo
            velocity.y += gravity * Time.deltaTime;
        }

        // CORRECCI�N: Aplicar movimiento vertical UNA SOLA VEZ
        controller.Move(new Vector3(0, velocity.y * Time.deltaTime, 0));
    }

    // M�todos p�blicos para obtener informaci�n
    public bool IsGrounded() { return controller.isGrounded; }
    public float GetCurrentSpeed() { return controller.velocity.magnitude; }
    public bool IsCrouching() { return isCrouching; }
}