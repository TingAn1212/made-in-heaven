using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

//Target.GetComponent<TextMeshProUGUI>().text;
public class VisionDescription : MonoBehaviour
{
    GameObject Target;
    public ChoiceControl control;
    string Tag = "Vision";
    string description = 
    @"Vision

Vision is incredibly flexble, allowing it to aim and move in totally different direction.
'E' put the engine on overdrive, temporarily increasing the maximum speed,
'Q' cloaks the ship and increase the FOV, allowing pilot to confuse the enemy and get out of sticky situation.
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
