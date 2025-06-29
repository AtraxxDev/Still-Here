using UnityEngine;
using System.Collections;
using TMPro;

public class TypewriterEffect
{
    private readonly TMP_Text _textComponent;
    private readonly float _charactersPerSecond;
    private Coroutine _typewriterCoroutine;

    public TypewriterEffect(TMP_Text textComponent, float charactersPerSecond = 20f)
    {
        _textComponent = textComponent;
        _charactersPerSecond = charactersPerSecond;
    }

    public void StartTypewriter(string text, MonoBehaviour coroutineRunner)
    {
        StopTypewriter(coroutineRunner);
        _typewriterCoroutine = coroutineRunner.StartCoroutine(TypewriterCoroutine(text));
    }

    public void StopTypewriter(MonoBehaviour coroutineRunner)
    {
        if (_typewriterCoroutine != null)
        {
            coroutineRunner.StopCoroutine(_typewriterCoroutine);
            _typewriterCoroutine = null;
        }
    }

    private IEnumerator TypewriterCoroutine(string text)
    {
        _textComponent.text = "";
        float delay = 1f / _charactersPerSecond;

        foreach (char c in text)
        {
            _textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}