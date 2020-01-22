using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.Playables;

/*permite controle no inspector*/
[System.Serializable]
public class Sound  
    /*permite visualização dos itens abaixo ?!*/
//   : MonoBehaviour
{
    public string name;

    public AudioClip clip;
    
    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

//    public AudioMixer output;

    [HideInInspector]
    public AudioSource source;
}
