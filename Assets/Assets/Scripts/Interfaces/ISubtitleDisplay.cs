using UnityEngine;

// Interfaz para mostrar subt�tulos
public interface ISubtitleDisplay
{
    void ShowSubtitle(string text, float duration);
    void HideSubtitle();
    bool IsShowingSubtitle();
}
