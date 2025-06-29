// ====================================
// SUBTITLE DISPLAY UI /// Solo maneja la UI de subtítulos
// ====================================
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SubtitleDisplayUI : MonoBehaviour, ISubtitleDisplay
{
    [Header("Referencias UI")]
    public TMP_Text subtitleText;
    public GameObject subtitlePanel;
    public CanvasGroup canvasGroup;

    [Header("Configuración")]
    public float fadeInDuration = 0.3f;
    public float fadeOutDuration = 0.5f;
    public bool autoHide = true;

    [Header("Typewriter Effect")]
    public bool useTypewriterEffect = true;
    public float charactersPerSecond = 20f;
    private TypewriterEffect _typewriterEffect;


    private Coroutine currentSubtitleCoroutine;
    private bool isShowingSubtitle = false;

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        // Obtener referencias automáticamente si no están asignadas
        if (subtitleText == null)
            subtitleText = GetComponentInChildren<TMP_Text>();

        if (subtitlePanel == null)
            subtitlePanel = gameObject;

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        // Ocultar subtítulos al inicio
        HideSubtitle();

        // Inicializar efecto de máquina de escribir
        _typewriterEffect = new TypewriterEffect(subtitleText, charactersPerSecond);
    }

    public void ShowSubtitle(string text, float duration)
    {
        if (string.IsNullOrEmpty(text)) return;

        // Detener corrutina anterior si existe
        if (currentSubtitleCoroutine != null)
        {
            StopCoroutine(currentSubtitleCoroutine);
        }

        currentSubtitleCoroutine = StartCoroutine(ShowSubtitleCoroutine(text, duration));
    }

    IEnumerator ShowSubtitleCoroutine(string text, float duration)
    {
        isShowingSubtitle = true;

        // Mostrar panel
        if (subtitlePanel != null)
            subtitlePanel.SetActive(true);

        // Fade in
        yield return StartCoroutine(FadeIn());

        // Mostrar texto con efecto de máquina de escribir
        if (useTypewriterEffect && subtitleText != null)
        {
            _typewriterEffect.StartTypewriter(text, this);

            // Calcular tiempo de escritura aproximado
            float typewriterDuration = text.Length / charactersPerSecond;
            yield return new WaitForSeconds(typewriterDuration);
        }
        else
        {
            // Mostrar texto directamente
            if (subtitleText != null)
                subtitleText.text = text;
        }

        // Esperar duración restante si es necesario
        if (autoHide && duration > 0)
        {
            float remainingDuration = duration - (useTypewriterEffect ? text.Length / charactersPerSecond : 0);
            if (remainingDuration > 0)
            {
                yield return new WaitForSeconds(remainingDuration);
            }

            // Fade out
            yield return StartCoroutine(FadeOut());

            // Ocultar panel
            if (subtitlePanel != null)
                subtitlePanel.SetActive(false);
        }

        isShowingSubtitle = false;
        currentSubtitleCoroutine = null;
    }

    IEnumerator FadeIn()
    {
        if (canvasGroup == null) yield break;

        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeInDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut()
    {
        if (canvasGroup == null) yield break;

        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    public void HideSubtitle()
    {
        if (currentSubtitleCoroutine != null)
        {
            StopCoroutine(currentSubtitleCoroutine);
            currentSubtitleCoroutine = null;
        }

        isShowingSubtitle = false;

        if (canvasGroup != null)
            canvasGroup.alpha = 0f;

        if (subtitlePanel != null)
            subtitlePanel.SetActive(false);

        // Detener efecto de máquina de escribir si está activo
        if (_typewriterEffect != null && useTypewriterEffect)
        {
            _typewriterEffect.StopTypewriter(this);
        }
    }

    public bool IsShowingSubtitle()
    {
        return isShowingSubtitle;
    }

    // Métodos públicos 
    public void SetFadeDuration(float fadeIn, float fadeOut)
    {
        fadeInDuration = fadeIn;
        fadeOutDuration = fadeOut;
    }

    public void SetAutoHide(bool autoHide)
    {
        this.autoHide = autoHide;
    }
}