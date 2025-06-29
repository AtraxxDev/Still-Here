using System;

// Interfaz para leer datos de di�logos
public interface IDialogueDataReader
{
    DialogueData GetDialogueById(string id);
    bool HasDialogue(string id);
}
