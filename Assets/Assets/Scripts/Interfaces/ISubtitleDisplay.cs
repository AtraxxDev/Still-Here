using UnityEngine;

// Interfaz para mostrar subtítulos
public interface ISubtitleDisplay
{
    void ShowSubtitle(string text, float duration);
    void HideSubtitle();
    bool IsShowingSubtitle();
}
