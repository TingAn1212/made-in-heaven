using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Apply : MonoBehaviour
{
    public Slider Music;
    public Slider Effect;
    public Slider Speed;
    public Slider Acc;
    // Start is called before the first frame update
    public void apply(){
        GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(0);
        PlayerPrefs.SetFloat("Music",Music.normalizedValue);
        PlayerPrefs.SetFloat("Effect",Effect.normalizedValue);
        PlayerPrefs.SetFloat("StartSpeed",(float)(Speed.normalizedValue*0.5f + 1));
        PlayerPrefs.SetFloat("TimeAcc",(float)(1 + Acc.normalizedValue));
        SceneManager.LoadScene("Menu");
    }
}
