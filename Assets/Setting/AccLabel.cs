using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class AccLabel : MonoBehaviour
{
    public GameObject Origin;
    Slider Target;
    // Start is called before the first frame update
    void Start()
    {
        Target = Origin.GetComponent<Slider>();
        Target.value = (PlayerPrefs.GetFloat("TimeAcc")-1f);
    }

    // Update is called once per frame
    void Update()
    {
        float value = Target.normalizedValue;
        float number = value + 1f;
        GetComponent<TextMeshProUGUI>().text = number.ToString("0.00");
    }
}
