using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndTextControl : MonoBehaviour
{
    public EndTextControl Next;
    public bool end = false;
    public bool Started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GoNext(){
        Next.Started = true;
    }
    void GoBack(){
        SceneManager.LoadScene("Menu");
    }
    // Update is called once per frame
    void Update()
    {
        if (Started){
            if (GetComponent<TextMeshProUGUI>().color.a < 1){
                Color old = GetComponent<TextMeshProUGUI>().color;
                old = new Color(old.r,old.g,old.b,old.a+(float)0.01);
                Color32 New = old;
                GetComponent<TextMeshProUGUI>().color = New;
            }else{
                Started = false;
                if (!end){
                    Invoke("GoNext", 2);
                }else{
                    Invoke("GoBack", 2);
                }
            }
            Debug.Log(GetComponent<TextMeshProUGUI>().color);
        }
    }
}
