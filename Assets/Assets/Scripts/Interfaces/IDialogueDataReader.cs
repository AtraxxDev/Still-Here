using System;

// Interfaz para leer datos de diálogos
public interface IDialogueDataReader
{
    DialogueData GetDialogueById(string id);
    bool HasDialogue(string id);
}
