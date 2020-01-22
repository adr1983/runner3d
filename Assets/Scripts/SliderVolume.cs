using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    public AudioMixer mixer;
//    public AudioManager mixer;
    public Slider slider;

    void Start()
    {
        /*define o volume do start som através do defaultValue porém, se ativo, seta denovo ao entrar em "main" e retornar para o menu"*/
//        slider.value = PlayerPrefs.GetFloat("SoundVol", 0.75f);
    }

    public void SetLevel()
    {
        /*codigo feito para fazer a transição do nivel do volume x nivel do slide, visto que um é aritmetico e o outro geometrico*/
        float sliderValue = slider.value;
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}