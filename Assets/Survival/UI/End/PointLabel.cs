using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int point = (int)PlayerPrefs.GetFloat("Point");
        GetComponent<TextMeshProUGUI>().text = point.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
