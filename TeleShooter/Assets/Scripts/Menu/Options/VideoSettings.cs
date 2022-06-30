using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoSettings : MonoBehaviour
{
    //Resolution
    public TMP_Dropdown resolutionDropdown;
    List<Resolution> useResolutions = new List<Resolution>();
    int resolutionIndex = 0;

    //Framerate
    public TMP_InputField frameRateInputField;

    //FullScreenMode
    public TMP_Dropdown fullScreenModeDropdown;

    private void Awake()
    {
        SelectFullScreenModeIndex();

        Resolution[] resolutions = Screen.resolutions;
        List<string> filteredResolutions = new List<string>();

        for (int i = resolutions.Length - 1; i >= 0; i--)
        {
            if (resolutions[i].refreshRate == Screen.currentResolution.refreshRate || resolutions[i].refreshRate == Screen.currentResolution.refreshRate - 1)
            {
                useResolutions.Add(resolutions[i]);
            }
        }

        //useResolutions.Reverse();

        for (int i = 0; i < useResolutions.Count; i++)
        {
            string option = useResolutions[i].width + "x" + useResolutions[i].height;
            filteredResolutions.Add(option);

            if (useResolutions[i].width == Screen.currentResolution.width && useResolutions[i].height == Screen.currentResolution.height)
            {
                resolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(filteredResolutions);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = useResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }

    public void SetFramerate(string fps)
    {
        try
        {
            Application.targetFrameRate = int.Parse(fps);
            frameRateInputField.text = fps;
        }
        catch { }
    }

    void SelectFullScreenModeIndex() 
    {
        if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            fullScreenModeDropdown.value = 0;
        }
        else if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            fullScreenModeDropdown.value = 1;
        }
        else if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            fullScreenModeDropdown.value = 2;
        }
    }

    public void SetFullScreenMode(int fullScreenModeIndex)
    {
        if (fullScreenModeIndex == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (fullScreenModeIndex == 1)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (fullScreenModeIndex == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
