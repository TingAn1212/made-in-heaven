using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectControl : MonoBehaviour
{
    public AudioClip[] Audios;
    static bool StartedEffect = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (!StartedEffect){
            DontDestroyOnLoad(this.gameObject);
            StartedEffect = true;
        }
    }
    public void Play(int index){
        GetComponent<AudioSource>().PlayOneShot(Audios[index],PlayerPrefs.GetFloat("Effect"));
    }
}
