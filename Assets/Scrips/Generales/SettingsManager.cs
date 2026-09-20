using System;
using UnityEngine;

public static class SettingsManager
{
    public static event Action<ModoControl> OnModoControlChanged;

    public static void CambiarModo(ModoControl nuevoModo)
    {

        Debug.Log("SettingsManager.CambiarModo -> " + nuevoModo);
        PlayerPrefs.SetInt("ModoControl", (int)nuevoModo);
        OnModoControlChanged?.Invoke(nuevoModo);
    }

    public static ModoControl CargarModoGuardado()
    {
        return (ModoControl)PlayerPrefs.GetInt("ModoControl", 0);
    }
}
