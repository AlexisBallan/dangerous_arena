using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class option : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Dropdown resolutionPossible;
    public InputField haut, bas, droite, gauche;
    public Slider slider;

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionPossible.ClearOptions();
        haut.text = PlayerPrefs.GetString("haut");
        bas.text = PlayerPrefs.GetString("bas");
        droite.text = PlayerPrefs.GetString("droite");
        gauche.text = PlayerPrefs.GetString("gauche");
        slider.value = PlayerPrefs.GetFloat("volume");


        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionPossible.AddOptions(options);
        resolutionPossible.value = currentResolutionIndex;
        resolutionPossible.RefreshShownValue();
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetToucheHaut(string touche)
    {
        PlayerPrefs.SetString("haut", touche);
    }
    public void SetToucheBas(string touche)
    {
        PlayerPrefs.SetString("bas", touche);
    }
    public void SetToucheDroite(string touche)
    {
        PlayerPrefs.SetString("droite", touche);
    }
    public void SetToucheGauche(string touche)
    {
        PlayerPrefs.SetString("gauche", touche);
    }
}
