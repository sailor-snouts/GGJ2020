using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class StartMenu : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] private string mainSceneName = "Main";
    [SerializeField] private string creditsSceneName = "Credits";
    [SerializeField] private GameObject options;
    [SerializeField] private AudioClip hitSFX;

    private void Start()
    {
        this.audio = this.GetComponent<AudioSource>();
    }

    public void Play()
    {
        this.audio.PlayOneShot(this.hitSFX);
        SceneManager.LoadSceneAsync(this.mainSceneName);
    }
    
    public void Credits()
    {
        this.audio.PlayOneShot(this.hitSFX);
        SceneManager.LoadSceneAsync(this.creditsSceneName);
    }

    public void Options()
    {
        this.audio.PlayOneShot(this.hitSFX);
        this.options.SetActive(true);
    }

    public void Quit()
    {     
#if UNITY_EDITOR
        this.audio.PlayOneShot(this.hitSFX);
        UnityEditor.EditorApplication.isPlaying = false;
#else
        this.audio.PlayOneShot(this.hitSFX);
          Application.Quit();
#endif
    }
}
