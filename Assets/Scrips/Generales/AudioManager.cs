using UnityEngine;
using UnityEngine.Audio;

// Representa un sonido con su nombre (para llamarlo) y su clip real.
[System.Serializable]
public class Sound
{
    public string nombre;   // ej: "MusicaFondo", "MusicaEvento", "Morir", "Saltar"
    public AudioClip[] clips;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Biblioteca de sonidos")]
    [SerializeField] private Sound[] sonidos;

    [Header("Canales de música (AudioSources para música)")]
    [SerializeField] private AudioSource[] canalesMusica; // normalmente con 2 te alcanza

    [Header("SFX")]
    [SerializeField] private AudioSource sfxSource;

    // Solo el canal 1 necesita "recordar" si lo detuviste a propósito,
    // porque es el único que tiene varias pistas random y se reinicia solo.
    private bool canal1Detenido = true;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Update()
    {
        // Solo reinicia el canal 1 si terminó SOLO (no si lo pausaste/detuviste tú)
        if (!canalesMusica[1].isPlaying && !canal1Detenido)
        {
            PlayMusic(1, "MusicaFondoJuego");
        }
    }

    // Busca un clip por nombre dentro de la biblioteca.
    private AudioClip BuscarClip(string nombre)
    {
        foreach (Sound s in sonidos)
        {
            if (s.nombre == nombre)
            {
                int indice = Random.Range(0, s.clips.Length);
                return s.clips[indice];
            }
        }
        Debug.LogWarning("No existe un sonido con nombre: " + nombre);
        return null;
    }

    // ---------- MÚSICA ----------
    // "canal" es la posición del AudioSource dentro de canalesMusica (0, 1, 2...)
    public void PlayMusic(int canal, string nombre)
    {
        AudioClip clip = BuscarClip(nombre);
        if (clip == null) return;

        canalesMusica[canal].clip = clip;
        //canalesMusica[canal].loop = true;
        canalesMusica[canal].Play();

        if (canal == 1) canal1Detenido = false; // se está reproduciendo, ya no está "detenido"
    }
    public void PauseMusic(int canal)
    {
        canalesMusica[canal].Pause();
        if (canal == 1) canal1Detenido = true;
    }

    public void ResumeMusic(int canal)
    {
        canalesMusica[canal].UnPause();
        if (canal == 1) canal1Detenido = false;
    }

    public void StopMusic(int canal)
    {
        canalesMusica[canal].Stop();
        if (canal == 1) canal1Detenido = true;
    }

    // ---------- SFX ----------
    public void PlaySFX(string nombre)
    {
        AudioClip clip = BuscarClip(nombre);
        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}