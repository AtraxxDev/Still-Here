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

    [Header("Configuraci�n de Zoom")]
    public float zoomFOV = 30f;
    public float zoomSpeed = 5f;
    public KeyCode zoomKey = KeyCode.Mouse1; // Bot�n derecho del rat�n

    private Transform playerBody;
    private Transform cameraTransform;
    private Camera playerCamera;
    private float xRotation = 0f;
    private float originalFOV;
    private float targetFOV;
    private bool isZooming = false;

    public void Initialize(Transform playerTransform, Transform camera)
    {
        playerBody = playerTransform;
        cameraTransform = camera;
        playerCamera = camera.GetComponent<Camera>();

        if (playerCamera != null)
        {
            originalFOV = playerCamera.fieldOfView;
            targetFOV = originalFOV;
        }

        // Bloquear cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HandleMouseInput(float mouseX, float mouseY, bool zoomInput)
    {
        if (playerBody == null || cameraTransform == null) return;

        // Manejar zoom
        isZooming = zoomInput;
        targetFOV = isZooming ? zoomFOV : originalFOV;

        if (playerCamera != null)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
        }

        // Aplicar sensibilidad modificada por zoom
        float currentSensitivity = isZooming ? mouseSensitivity * 0.5f : mouseSensitivity;

        mouseX *= currentSensitivity * Time.deltaTime;
        mouseY *= currentSensitivity * Time.deltaTime;

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

    public bool IsZooming() { return isZooming; }
}