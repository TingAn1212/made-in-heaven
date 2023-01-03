using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Target.GetComponent<TextMeshProUGUI>().text;
public class ZipDescription : MonoBehaviour
{
    GameObject Target;
    public ChoiceControl control;
    string Tag = "Zip";
    string description = 
    @"Zip

Zip is very agile, it's advanced radar give pilot a larger FOV.
'E' immediately teleport the ship to the current mouse position.
'Q' activates a time shield, slowing down time temporarily. 
    ";
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Description");
    }
    public void Pressed(){
        Target.GetComponent<TextMeshProUGUI>().text = description;
        control.Tag = Tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
