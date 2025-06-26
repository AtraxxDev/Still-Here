// ====================================
// JSON DIALOGUE READER /// Solo lee y parsea JSON
// ====================================
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class JsonDialogueReader : MonoBehaviour, IDialogueDataReader
{
    [Header("Configuración")]
    public string jsonFileName = "dialogues.json";
    public bool loadFromStreamingAssets = true;
    public bool loadFromResources = false;

    private Dictionary<string, DialogueData> dialogueDictionary;
    private bool isInitialized = false;

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (isInitialized) return;

        LoadDialoguesFromJson();
        isInitialized = true;
    }

    void LoadDialoguesFromJson()
    {
        dialogueDictionary = new Dictionary<string, DialogueData>();
        string jsonContent = "";

        try
        {
            if (loadFromStreamingAssets)
            {
                jsonContent = LoadFromStreamingAssets();
            }
            else if (loadFromResources)
            {
                jsonContent = LoadFromResources();
            }

            if (!string.IsNullOrEmpty(jsonContent))
            {
                ParseJsonContent(jsonContent);
                Debug.Log($"Diálogos cargados: {dialogueDictionary.Count}");
            }
            else
            {
                Debug.LogWarning("No se pudo cargar el archivo JSON de diálogos");
                CreateDefaultDialogues();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al cargar diálogos: {e.Message}");
            CreateDefaultDialogues();
        }
    }

    string LoadFromStreamingAssets()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }

        Debug.LogWarning($"Archivo no encontrado en StreamingAssets: {filePath}");
        return "";
    }

    string LoadFromResources()
    {
        string fileName = Path.GetFileNameWithoutExtension(jsonFileName);
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        if (jsonFile != null)
        {
            return jsonFile.text;
        }

        Debug.LogWarning($"Archivo no encontrado en Resources: {fileName}");
        return "";
    }

    void ParseJsonContent(string jsonContent)
    {
        DialogueCollection collection = JsonUtility.FromJson<DialogueCollection>(jsonContent);

        if (collection != null && collection.dialogues != null)
        {
            foreach (DialogueData dialogue in collection.dialogues)
            {
                if (!string.IsNullOrEmpty(dialogue.id))
                {
                    dialogueDictionary[dialogue.id] = dialogue;
                }
            }
        }
    }

    void CreateDefaultDialogues()
    {
        // Crear algunos diálogos por defecto para testing
        dialogueDictionary = new Dictionary<string, DialogueData>
        {
            ["intro_01"] = new DialogueData("intro_01", "¿Dónde estoy? Este lugar me resulta familiar...", 4f, "", "Protagonista"),
            ["memory_01"] = new DialogueData("memory_01", "Recuerdo haber estado aquí antes, pero ¿cuándo?", 3.5f, "", "Protagonista"),
            ["fear_01"] = new DialogueData("fear_01", "Algo no está bien en este lugar...", 3f, "", "Protagonista"),
            ["test"] = new DialogueData("test", "Este es un diálogo de prueba", 2f, "", "Sistema")
        };

        Debug.Log("Diálogos por defecto creados");
    }

    // Implementación de la interfaz
    public DialogueData GetDialogueById(string id)
    {
        if (!isInitialized)
            Initialize();

        if (dialogueDictionary.ContainsKey(id))
        {
            return dialogueDictionary[id];
        }

        Debug.LogWarning($"Diálogo con ID '{id}' no encontrado");
        return null;
    }

    public bool HasDialogue(string id)
    {
        if (!isInitialized)
            Initialize();

        return dialogueDictionary.ContainsKey(id);
    }

    // Métodos públicos
    public int GetDialogueCount()
    {
        return dialogueDictionary?.Count ?? 0;
    }

    public void ReloadDialogues()
    {
        isInitialized = false;
        Initialize();
    }
}