using Sirenix.OdinInspector;
using UnityEngine;
using TGDebugColors;


// Script encargado de todo al audio del juego tanto musica como efectos de sonido
public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [Title("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Title("Audio Data")]
    public AudioDataSO audioData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioData.Initialize();
            LoadVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reproduce la musica mediante su string como se encuentra en el diccionario ejemplo PlayMusic("Ambiental_1");
    public void PlayMusic(string name, bool loop = true)
    {
        var clip = audioData.GetMusicClip(name);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.pitch = 1f;
            musicSource.Play();
        }
        else
        {
            DebugColors.printLogWarning($"[AudioManager] Music clip '{name}' not found.");
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (!musicSource.isPlaying && musicSource.clip != null)
        {
            musicSource.UnPause(); // Reanuda desde donde se pausó
        }
    }

    // Reproduce un efecto de sonido mediante su string como se encuentra en el diccionario ejemplo PlaySFX("JumpScare");
    public void PlaySFX(string name)
    {
        var clip = audioData.GetSFXClip(name);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            DebugColors.printLogWarning($"[AudioManager] SFX clip '{name}' not found.");
        }
    }

    // Reproduce un efecto de sonido aleatorio entre ciertos valores ejemplo: PlayRandomSFX("Scare_1","Scare_2" etc);
    public void PlayRandomSFX(params string[] names)
    {
        if (names == null || names.Length == 0) return;
        PlaySFX(names[Random.Range(0, names.Length)]);
    }

    // Asigna el valor del volumen a los AudoSources para llamarlos desde otro script (ejemplo: settings)
    [Button]
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        DebugColors.printColor($"Nuevo valor de audio: {volume}",DebugColors.ORANGE);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    // Obtienen el valor del volumen de los AudoSources para llamarlos desde otro script (ejemplo: settings)
    public float GetMusicVolume() => musicSource.volume;
    public float GetSFXVolume() => sfxSource.volume;

    // Carga el volumen por defecto de los player prefs y lo coloca en 1 o en el valor deseado
    void LoadVolumes()
    {
        DebugColors.printSuccess("Se cargaron los datos del audio");

        SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("sfxVolume", 1f));
    }

}
