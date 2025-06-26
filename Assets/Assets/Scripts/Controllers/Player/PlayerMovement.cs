// ====================================
// PLAYER MOVEMENT (M�dulo de Movimiento)
// ====================================
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;

    public void Initialize()
    {
        controller = GetComponent<CharacterController>();
    }

    public void HandleMovement(float horizontal, float vertical, bool isRunning)
    {
        // Calcular direcci�n de movimiento
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Determinar velocidad
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Aplicar movimiento horizontal
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Aplicar gravedad
        ApplyGravity();
    }

    void ApplyGravity()
    {
        // Verificar si est� en el suelo
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // M�todos p�blicos para obtener informaci�n
    public bool IsGrounded() { return controller.isGrounded; }
    public float GetCurrentSpeed() { return controller.velocity.magnitude; }
}