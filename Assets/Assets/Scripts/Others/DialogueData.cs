// ====================================
// MODELOS DE DATOS /// Solo contiene datos
// ====================================
using System;

[Serializable]
public class DialogueData
{
    public string id;
    public string text;
    public float duration;
    public string audioClip;
    public string speaker;

    public DialogueData(string id, string text, float duration = 3f, string audioClip = "", string speaker = "")
    {
        this.id = id;
        this.text = text;
        this.duration = duration;
        this.audioClip = audioClip;
        this.speaker = speaker;
    }
}

[Serializable]
public class DialogueCollection
{
    public DialogueData[] dialogues;
}