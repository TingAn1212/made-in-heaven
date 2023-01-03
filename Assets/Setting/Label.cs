using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Label : MonoBehaviour
{
    public GameObject Origin;
    public string key;
    Slider Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = Origin.GetComponent<Slider>();
        Target.value = PlayerPrefs.GetFloat(key);
    }

    // Update is called once per frame
    void Update()
    {
        float value = Target.normalizedValue;
        int percent = (int)Mathf.Round(value*100);
        string text = percent.ToString() + "%";
        GetComponent<TextMeshProUGUI>().text = text;
    }
}
