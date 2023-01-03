using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    public bool Started;
    public GameControl control;
    // Start is called before the first frame update
    void Start()
    {
        Started = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Started){
            if (GetComponent<Image>().color.a < 1){
                GetComponent<Image>().color = new Color(0,0,0,GetComponent<Image>().color.a + (float)0.01);
            }else{
                Started = false;
                SceneManager.LoadScene("End");
            }
        }
    }
}

