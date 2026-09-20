using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    // Referencia al AudioMixer que creaste en el proyecto (el asset con los grupos Master/SFX/Music)
    [SerializeField] AudioMixer audioMixer;
    // Los 3 sliders de tu panel de Settings, para poder leerlos y también moverlos por código
    [Header("Sliders (arrástralos aquí para cargar su valor guardado)")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;

    // Nombres "clave" con los que se guarda y se busca cada valor.
    // Uso constantes (const) en vez de escribir el string 3 veces, así si me equivoco de nombre, 
    // el error sale en un solo lugar y no en cada método por separado.
    private const string KEY_MASTER = "MasterVolume";
    private const string KEY_SFX = "SFXVolume";
    private const string KEY_MUSIC = "MusicVolume";
    //const = "este valor no cambia nunca, lo defino una sola vez arriba, y lo reutilizo por nombre en el resto del código" — te protege de errores de tipeo y hace que si algún día necesitas cambiar el texto, lo cambias en un solo lugar en vez de buscar en todo el archivo.

    // Start() se ejecuta automáticamente UNA vez, apenas arranca la escena.
    // Acá es donde "recuperamos" el volumen que el jugador había dejado la vez anterior.
    private void Start()
    {
        // PlayerPrefs.GetFloat("clave", valorPorDefecto)
        // Busca en el disco del dispositivo si existe un valor guardado con esa clave.
        // Si nunca se guardó nada (primera vez que se abre el juego), usa 0.5f.
        float master = PlayerPrefs.GetFloat(KEY_MASTER, 0.5f);//el valor 0.5 sera el defecto por primera ves despues cuadno se tome, tomara el que se guardo y ese se ignora el 0.5
        float sfx = PlayerPrefs.GetFloat(KEY_SFX, 0.5f);
        float music = PlayerPrefs.GetFloat(KEY_MUSIC, 0.5f);

        // Aplica esos valores al AudioMixer real, para que el sonido suene como corresponde
        SetMasterVolume(master);
        SetSFXVolume(sfx);
        SetMusicVolume(music);

        // Mueve la "bolita" del slider a esa posición, para que el jugador VEA el volumen correcto.
        // Uso SetValueWithoutNotify en vez de "sliderMaster.value = master" porque .value normal
        // dispara el evento OnValueChanged del slider (como si el jugador lo hubiera movido a mano),
        // lo cual llamaría de nuevo a SetMasterVolume innecesariamente. SetValueWithoutNotify mueve
        // el slider SIN disparar ese evento.
        sliderMaster.SetValueWithoutNotify(master);
        sliderSFX.SetValueWithoutNotify(sfx);
        sliderMusic.SetValueWithoutNotify(music);
    }

    // Se llama automáticamente cuando la app pasa a segundo plano (el usuario sale de la app)
    // o vuelve a primer plano. En celular, "pause = true" es básicamente cada vez que el usuario
    // aprieta Home, recibe una llamada, cambia de app, etc.
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // Fuerza a escribir en el disco YA, en vez de esperar a un cierre "normal"
            // que en celular puede no llegar a pasar nunca.
            PlayerPrefs.Save();
        }
    }

    // Estos 3 métodos son los que ya tenías conectados en el OnValueChanged de cada slider.
    // Cada uno hace 2 cosas: cambia el sonido AHORA, y GUARDA el valor para la próxima vez.
    public void SetMasterVolume(float level)
    {
        // El AudioMixer no trabaja con valores de 0 a 1, trabaja en decibeles (escala logarítmica).
        // Por eso se convierte con Mathf.Log10(level) * 20f antes de pasarlo al Mixer.
        audioMixer.SetFloat(KEY_MASTER, Mathf.Log10(level) * 20f);

        // PlayerPrefs.SetFloat guarda el valor "crudo" (0 a 1, el mismo que trae el slider)
        // en el disco del dispositivo, para que sobreviva a reinicios de escena o del juego.
        PlayerPrefs.SetFloat(KEY_MASTER, level);
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat(KEY_SFX, Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(KEY_SFX, level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat(KEY_MUSIC, Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(KEY_MUSIC, level);
    }
}