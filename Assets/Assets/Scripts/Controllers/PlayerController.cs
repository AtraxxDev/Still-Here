// ====================================
// PLAYER CONTROLLER (Controlador Principal)
// ====================================
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Referencias a Módulos")]
    public PlayerMovement playerMovement;
    public PlayerInteraction playerInteraction;
    public PlayerMouseController playerMouseController;

    [Header("Configuración Global")]
    public bool enableMovement = true;
    public bool enableInteraction = true;
    public bool enableMouseControl = true;

    void Start()
    {
        InitializeModules();
    }

    void Update()
    {
        HandleInput();
    }

    void InitializeModules()
    {
        // Obtener referencias automáticamente si no están asignadas
        if (playerMovement == null)
            playerMovement = GetComponent<PlayerMovement>();

        if (playerInteraction == null)
            playerInteraction = GetComponent<PlayerInteraction>();

        if (playerMouseController == null)
            playerMouseController = GetComponent<PlayerMouseController>();

        // Inicializar módulos
        if (playerMovement != null)
            playerMovement.Initialize();

        if (playerInteraction != null)
            playerInteraction.Initialize(Camera.main.transform);

        if (playerMouseController != null)
            playerMouseController.Initialize(transform, Camera.main.transform);
    }

    void HandleInput()
    {
        // Control de ratón
        if (enableMouseControl && playerMouseController != null)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            playerMouseController.HandleMouseInput(mouseX, mouseY);
        }

        // Movimiento
        if (enableMovement && playerMovement != null)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            playerMovement.HandleMovement(horizontal, vertical, isRunning);
        }

        // Interacción
        if (enableInteraction && playerInteraction != null)
        {
            playerInteraction.CheckForInteractables();

            if (Input.GetKeyDown(KeyCode.E))
            {
                playerInteraction.TryInteract();
            }
        }
    }

    // Métodos públicos para controlar módulos
    public void EnableMovement(bool enable) { enableMovement = enable; }
    public void EnableInteraction(bool enable) { enableInteraction = enable; }
    public void EnableMouseControl(bool enable) { enableMouseControl = enable; }
    public void EnableAllModules(bool enable) { enableMovement = enableInteraction = enableMouseControl = enable; }
}