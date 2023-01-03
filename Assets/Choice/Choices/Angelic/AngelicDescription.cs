using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Target.GetComponent<TextMeshProUGUI>().text;
public class AngelicDescription : MonoBehaviour
{
    GameObject Target;
    public ChoiceControl control;
    string Tag = "Angelic";
    string description = 
    @"Angelic

Angelic enjoys enhanced repair, allowing it to heal more when picking up orbs.
'E' ability triggers active repair, restoring HP,
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