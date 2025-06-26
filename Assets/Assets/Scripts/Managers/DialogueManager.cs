// ====================================
// DIALOGUE MANAGER /// Solo coordina entre lector y display
// ====================================
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Referencias")]
    public JsonDialogueReader dialogueReader;
    public SubtitleDisplayUI subtitleDisplay;

    [Header("Configuraci�n")]
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
        // Obtener referencias autom�ticamente si no est�n asignadas
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

    // M�todo principal para mostrar di�logos
    public bool ShowDialogue(string dialogueId)
    {
        if (dataReader == null || subtitleDisplayInterface == null)
        {
            Debug.LogError("DialogueManager no est� inicializado correctamente");
            return false;
        }

        DialogueData dialogue = dataReader.GetDialogueById(dialogueId);

        if (dialogue != null)
        {
            subtitleDisplayInterface.ShowSubtitle(dialogue.text, dialogue.duration);
            Debug.Log($"Mostrando di�logo: {dialogue.speaker} - {dialogue.text}");
            return true;
        }

        return false;
    }

    // M�todos p�blicos adicionales
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

    // M�todos de conveniencia para diferentes tipos de di�logos
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