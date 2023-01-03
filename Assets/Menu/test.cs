using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        int width = Screen.width;
        int height = Screen.height;
        //Debug.Log(width.ToString() + ", " + height.ToString());
        //Debug.Log(mousePos);
    }
}
