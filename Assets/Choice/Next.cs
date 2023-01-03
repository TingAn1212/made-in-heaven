using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public ChoiceControl control;
    public void Presses(){
        GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(0);
        PlayerPrefs.SetString("Choice",control.Tag);
        SceneManager.LoadScene("Survival");
    }
}
