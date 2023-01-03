using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Target.GetComponent<TextMeshProUGUI>().text;
public class FortressDescription : MonoBehaviour
{
    GameObject Target;
    public ChoiceControl control;
    string Tag = "Fortress";
    string description = 
    @"Fortress

Fortress only absorb 50% of damage taken, allowing it to soak up huge amount of attacks.
'E' ability creates a shield, blocking all projectiles,
'Q' creates a modified shield, reflecting all projectiles.
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