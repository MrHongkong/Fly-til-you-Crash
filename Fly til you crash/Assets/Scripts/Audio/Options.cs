using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Options : MonoBehaviour
{
    
    public Slider slider;
    public AudioMixer audioMixer;

    void Start()
    {
        if (slider == null) slider = GetComponentInChildren<Slider>();
        if (slider != null) slider.value = PlayerPrefs.GetFloat("Volume");
        GetMasterLevel();
    }
       
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSlider()
    {
        slider = GameObject.Find("Menu").GetComponentInChildren<Slider>();
        if (slider != null) slider.value = PlayerPrefs.GetFloat("Volume");
    }

    public float GetMasterLevel()
    {
        float value;
        bool result = audioMixer.GetFloat("Volume", out value);
        if (result)
        {
            return value;
            
        }
        else
        {
            return 0f;
        }
        
    }


}
