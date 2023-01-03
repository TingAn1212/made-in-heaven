using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Floaty : MonoBehaviour
{
    float pos = -1f;
    float direction = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos += direction;
        if (pos >= 60){
            direction = -1f;
        }
        if (pos <= -60){
            direction = 1f;
        }
        GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x,(pos/4f)+Screen.height/2f,GetComponent<RectTransform>().position.z);
        if (Input.GetMouseButtonDown(0)){
            SceneManager.LoadScene("Menu");
        }
    }
}
