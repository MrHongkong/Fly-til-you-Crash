using UnityEngine;
using UnityEngine.UI;

public class Setter : MonoBehaviour
{
    AudioManager set;
    public Slider menuSlider;

    private void Start()
    {
        set = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        set.SetSlider();
    }
}