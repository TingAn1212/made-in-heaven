using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControl : MonoBehaviour
{
    public AudioClip[] Audios;

    static bool StartedMusic = false;
    static string Playing;
    // Start is called before the first frame update
    void Awake()
    {
        if (!StartedMusic){
            DontDestroyOnLoad(this.gameObject);
            StartedMusic = true;
        }
    }
    void UpdateVolume(){
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music");
    }
    void Update(){
        UpdateVolume();
        string name = SceneManager.GetActiveScene().name;
        if (name == "Menu"){
            if (Playing != "Menu"){
                Playing = "Menu";
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = Audios[0];
                GetComponent<AudioSource>().Play();
            }
        }
        if (name == "Tutorial"){
            if (Playing != "Tutorial"){
                Playing = "Tutorial";
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = Audios[1];
                GetComponent<AudioSource>().Play();
            }
        }
        if (name == "Survival"){
            if (Playing != "Survial"){
                Playing = "Survial";
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = Audios[2];
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
