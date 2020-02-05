using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private Resolution[] resolutions;
    [SerializeField] private GameObject selected;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer mixer;

    public void OnEnable()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(this.selected);
    }

    public void Start()
    {
        this.resolutions = Screen.resolutions;
        this.resolutionDropdown.ClearOptions();
        int currentIndex = 0;
        List<String> options = new List<string>();
        for (int i = 0; i < this.resolutions.Length; i++)
        {
            string option = this.resolutions[i].width + "x" + this.resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentIndex = i;
            }
        }
        this.resolutionDropdown.AddOptions(options);
        this.resolutionDropdown.value = currentIndex;
        this.resolutionDropdown.RefreshShownValue();
        
        this.qualityDropdown.value = QualitySettings.GetQualityLevel();
        this.qualityDropdown.RefreshShownValue();
    }

    public void FullScreen(bool enabled)
    {
        Screen.fullScreen = enabled;
    }

    public void SetVolumeMaster(float volume)
    {
        this.mixer.SetFloat("Master", volume);    
    }

    public void SetVolumeMusic(float volume)
    {
        this.mixer.SetFloat("Music", volume);    
    }

    public void SetVolumeSFX(float volume)
    {
        this.mixer.SetFloat("SFX", volume);    
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetGraphics(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
