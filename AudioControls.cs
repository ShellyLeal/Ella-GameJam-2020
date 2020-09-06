using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControls : MonoBehaviour
{
    public AudioSource self;
    static bool isMute = false;
    public GameObject btnSound, btnMute;
    public AudioListener cameraListener;

    private void Awake()
    {
        cameraListener.enabled = !isMute;
    }

    public void StartCheck(bool i)
    {
        if (i && !isMute)
        {
            btnMute.SetActive(true);
            btnSound.SetActive(false);
            StartCoroutine(FadeIn());
        }
        else if(!isMute)
        {
            btnMute.SetActive(false);
            btnSound.SetActive(true);
            StartCoroutine(FadeOut());
        }
    }

    public void AdjustVolume(bool i)
    {
        if (i && !isMute)
        {
            StartCoroutine(FadeIn());
        }
        else if(!i)
        {
            StartCoroutine(FadeOut());
        }
    }

    public void setMute(bool muteValue)
    {
        isMute = muteValue;
        cameraListener.enabled = !isMute;
    }

    private IEnumerator FadeIn()
    {
        float volume = self.volume;
        float volumeEndValue = 0;
        while (volume >= volumeEndValue)
        {
            SetSoundVolume(ref volume, -1);
            yield return null;
        }
        self.volume = volumeEndValue;
    }

    private IEnumerator FadeOut()
    {
        float volume = self.volume;
        float volumeEndValue = 0.5f;
        while (volume <= volumeEndValue)
        {
            SetSoundVolume(ref volume, 1);
            yield return null;
        }
        if (!self.isPlaying)
        {
            self.Play();
        }
        self.volume = volumeEndValue;
    }

    private void SetSoundVolume(ref float volume, int Dir)
    {
        self.volume = volume;
        volume += Time.deltaTime * (1.0f / 1.0f) * (Dir);
    }
}
