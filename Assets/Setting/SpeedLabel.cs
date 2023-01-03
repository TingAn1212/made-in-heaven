using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SpeedLabel : MonoBehaviour
{
    public GameObject Origin;
    Slider Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = Origin.GetComponent<Slider>();
        Target.value = (PlayerPrefs.GetFloat("StartSpeed")-1f)/0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float value = Target.normalizedValue;
        float number = (value * 0.5f) + 1f;
        GetComponent<TextMeshProUGUI>().text = number.ToString("0.00");
    }
}
