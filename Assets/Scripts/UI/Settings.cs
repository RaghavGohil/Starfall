using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

internal sealed class Settings : MonoBehaviour,ISave
{

    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle graphicsToggle;

    private void Start()
    {
        LoadData();
    }

    internal void UpdateMusic()
    {
        if (musicToggle.isOn)
        {

        }
        else 
        {
        
        }
    }

    internal void UpdateSound()
    {
        if (soundToggle.isOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    internal void UpdateGraphics()
    {
        if (graphicsToggle.isOn)
        {
            QualitySettings.SetQualityLevel(2); // Medium
        }
        else
        {
            QualitySettings.SetQualityLevel(0); // Very Low
        }
    }

    public void LoadData() 
    {
        object o = SerializationManager.Load("settings");
        if(o != null)
        {
            bool[] toggle_settings = (bool[])o;
            graphicsToggle.isOn = toggle_settings[0];
            musicToggle.isOn = toggle_settings[1];
            soundToggle.isOn = toggle_settings[2];
            UpdateSettings();
        }
    }

    public void SaveData() 
    {
        bool[] toggle_settings = { graphicsToggle.isOn, musicToggle.isOn, soundToggle.isOn };
        SerializationManager.Save("settings",toggle_settings);
        UpdateSettings();
    }

    internal void UpdateSettings() 
    {
        UpdateSound();
        UpdateGraphics();
        UpdateMusic();
    }

}
