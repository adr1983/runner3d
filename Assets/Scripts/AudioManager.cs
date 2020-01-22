using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.Playables;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    
    public AudioMixer musicMixer;

    void Awake()
    {
        /*garante que ocorra só uma instancia de audiomanager nas trocas de scene*/
        if (instance == null)
            instance = this;
        else
        {
            Destroy((gameObject));
            return;
        }
        
        foreach(Sound s in sounds)
        {
            /*garante que a musica permaneça entre as trocas de scenes*/
//            DontDestroyOnLoad(gameObject);
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
//            s.source.outputAudioMixerGroup = s.source;
        }
    }

    void Start()
    {
        Play("Theme");
        
    }
    //  metodo utilizado para tocar a musica atraves da string atribuida
    //  com as características criadas dentro do awake anterior
    public void Play (string name)
    {  
        // encontra o som, dentro da array sounds que contem o nome do som igual ao da string do metodo
        Sound s = Array.Find(sounds, sound => sound.name == name);
        /*evita dar erro se o nome da musica nao tiver na lista*/
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!!!");
            return;
        }

        // Debug.Log("name detectado FX =" + name);
        s.source.Play();
    }
}








