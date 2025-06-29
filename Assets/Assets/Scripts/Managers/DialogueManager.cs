// ====================================
// DIALOGUE MANAGER /// Solo coordina entre lector y display
// ====================================
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Referencias")]
    public JsonDialogueReader dialogueReader;
    public SubtitleDisplayUI subtitleDisplay;

    [Header("Configuración")]
    public bool initializeOnStart = true;

    private IDialogueDataReader dataReader;
    private ISubtitleDisplay subtitleDisplayInterface;

    void Start()
    {
        if (initializeOnStart)
            Initialize();
    }

    public void Initialize()
    {
        // Obtener referencias automáticamente si no están asignadas
        if (dialogueReader == null)
            dialogueReader = FindObjectOfType<JsonDialogueReader>();

        if (subtitleDisplay == null)
            subtitleDisplay = FindObjectOfType<SubtitleDisplayUI>();

        // Configurar interfaces (SOLID - Dependency Inversion)
        dataReader = dialogueReader;
        subtitleDisplayInterface = subtitleDisplay;

        if (dataReader == null || subtitleDisplayInterface == null)
        {
            Debug.LogError("DialogueManager: Faltan referencias necesarias");
        }
    }

    // Método principal para mostrar diálogos
    public bool ShowDialogue(string dialogueId)
    {
        if (dataReader == null || subtitleDisplayInterface == null)
        {
            Debug.LogError("DialogueManager no está inicializado correctamente");
            return false;
        }

        DialogueData dialogue = dataReader.GetDialogueById(dialogueId);

        if (dialogue != null)
        {
            subtitleDisplayInterface.ShowSubtitle(dialogue.text, dialogue.duration);
            Debug.Log($"Mostrando diálogo: {dialogue.speaker} - {dialogue.text}");
            return true;
        }

        return false;
    }

    // Métodos públicos adicionales
    public bool HasDialogue(string dialogueId)
    {
        return dataReader?.HasDialogue(dialogueId) ?? false;
    }

    public void HideCurrentDialogue()
    {
        subtitleDisplayInterface?.HideSubtitle();
    }

    public bool IsShowingDialogue()
    {
        return subtitleDisplayInterface?.IsShowingSubtitle() ?? false;
    }

    public DialogueData GetDialogueData(string dialogueId)
    {
        return dataReader?.GetDialogueById(dialogueId);
    }

    // Métodos de conveniencia para diferentes tipos de diálogos
    public void ShowMemoryDialogue(string memoryId)
    {
        ShowDialogue($"memory_{memoryId}");
    }

    public void ShowIntroDialogue(string introId)
    {
        ShowDialogue($"intro_{introId}");
    }

    public void ShowFearDialogue(string fearId)
    {
        ShowDialogue($"fear_{fearId}");
    }
}