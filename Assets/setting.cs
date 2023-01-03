using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class setting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        if (PlayerPrefs.GetInt("FirstTime") != 69){
            PlayerPrefs.SetInt("FirstTime",69);
            PlayerPrefs.SetFloat("Music",(float)1);
            PlayerPrefs.SetFloat("Effect",(float)1);
            PlayerPrefs.SetString("Choice","");
            PlayerPrefs.SetFloat("StartSpeed",1f);
            PlayerPrefs.SetFloat("TimeAcc",1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
// 440:1030
// To manually configure the canvas size, you must first disable the automatic size synchronization. 
// To do so, in the index.html file of the WebGL template, 
// set the Unity Instance configuration variable to false: 
// matchWebGLToCanvasSize=false
