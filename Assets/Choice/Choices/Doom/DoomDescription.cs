using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Target.GetComponent<TextMeshProUGUI>().text;
public class DoomDescription : MonoBehaviour
{
    GameObject Target;
    public ChoiceControl control;
    string Tag = "Doom";
    string description = 
    @"Doom

Doom have very high firepower and defense, while suffering from a slower speed.
'E' ability launches a spray of shells, damaging enemy infront,
'Q' ability activate killing mode, removing the cooldown of 'E' ability for a while.
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
