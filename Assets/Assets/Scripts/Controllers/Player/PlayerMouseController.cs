// ====================================
// PLAYER MOUSE CONTROLLER (M�dulo de Control de Rat�n)
// ====================================
using UnityEngine;

public class PlayerMouseController : MonoBehaviour
{
    [Header("Configuraci�n de Sensibilidad")]
    public float mouseSensitivity = 100f;

    [Header("L�mites de Rotaci�n")]
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

        // Rotaci�n vertical (c�mara)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotaci�n horizontal (jugador)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // M�todos p�blicos para controlar el cursor
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