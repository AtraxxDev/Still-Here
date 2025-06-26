// ====================================
// PLAYER MOUSE CONTROLLER (Módulo de Control de Ratón)
// ====================================
using UnityEngine;

public class PlayerMouseController : MonoBehaviour
{
    [Header("Configuración de Sensibilidad")]
    public float mouseSensitivity = 100f;

    [Header("Límites de Rotación")]
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private Transform playerBody;
    private Transform cameraTransform;
    private float xRotation = 0f;

    public void Initialize(Transform playerTransform, Transform camera)
    {
        playerBody = playerTransform;
        cameraTransform = camera;

        // Bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleMouseInput(float mouseX, float mouseY)
    {
        if (playerBody == null || cameraTransform == null) return;

        // Aplicar sensibilidad y deltaTime
        mouseX *= mouseSensitivity * Time.deltaTime;
        mouseY *= mouseSensitivity * Time.deltaTime;

        // Rotación vertical (cámara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal (jugador)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Métodos públicos para controlar el cursor
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetSensitivity(float sensitivity)
    {
        mouseSensitivity = sensitivity;
    }
}