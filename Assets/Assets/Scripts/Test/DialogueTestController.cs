// ====================================
// EJEMPLO DE USO Y TESTING
// ====================================
using UnityEngine;

public class DialogueTestController : MonoBehaviour
{
    [Header("Referencias")]
    public DialogueManager dialogueManager;

    [Header("Testing")]
    public string testDialogueId = "test";

    void Start()
    {
        if (dialogueManager == null)
            dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        // Teclas de prueba
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dialogueManager.ShowDialogue("intro_01");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            dialogueManager.ShowDialogue("memory_01");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            dialogueManager.ShowDialogue("fear_01");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            dialogueManager.ShowDialogue(testDialogueId);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            dialogueManager.HideCurrentDialogue();
        }
    }
}