using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TextBoxScript : MonoBehaviour
{
    public GameObject Text;
    public CoverControl Cover;
    public TutorialControl control;
    int Timer = 0;
    bool TimerStart = false;
    public bool Next = false;
    string[] Data = {
        "Welcome to the simulation, pilot.\n\n(Press z to continue)",
        "The gas giant 'Heaven' is currently under invasion from an unknown alien species.",
        "You are part of a special task force to investigate this planet.",
        "Stay alive for as long as possible, the soldiers on the frontline can receive valuable information from you.",
        "...",
        "This will be your last training here, treasure your last moment, pilot.",
        "...",
        "Lets get started, use the 'w,a,s,d' keys to move:",
        "Different ship will have different speed and move differently, so you will need time to test each of them out.",
        "'Vision' is the name of the ship that you are currently using, it have the unique ability to hover,",
        "Most ship can only face and shoot at the direction that is it moving towards, however, 'vision' runs on blade propellers, hence it can move and face totally different direction.",
        "With movement out of the way, we should test out the weapon system,",
        "Each ship have a primary and secondary weapon, as well as two abilities",
        "Left mouse button is linked to the primary weapon, it launches a high speed projectile that damages entity that it collided with.",
        "Right mouse button launches a slower projectile, but it have a higher damage.",
        "If you are moving at a very high speed, the trajectory of projectiles will be distorted.",
        "The 'E' key triggers your primary ability, the ability of 'vision' is a temporary speed boost",
        "The 'Q' key holds your secondary ability, 'vision' can turn itself invisible while enjoying an expanded field of view.",
        "However, take note that all ability and weapon have cool down, so use them wisely.",
        "Now, lets try destorying this dummy.",
        "Good job, you have finished the tutorial.",
        "Battlefield awaits..."
    };
    public int Index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Text.GetComponent<TextMeshProUGUI>().text = Data[Index];
    }

    public void appear(){
        GetComponent<Image>().color = new Color(1,1,1,1);
        Color old = Text.GetComponent<TextMeshProUGUI>().color;
        old = new Color(old.r,old.g,old.b,1);
        Color32 New = old;
        Text.GetComponent<TextMeshProUGUI>().color = New;
    }
    public void hide(){
        GetComponent<Image>().color = new Color(1,1,1,0);
        Color old = Text.GetComponent<TextMeshProUGUI>().color;
        old = new Color(old.r,old.g,old.b,0);
        Color32 New = old;
        Text.GetComponent<TextMeshProUGUI>().color = New;
    }
    public void update(){
        Text.GetComponent<TextMeshProUGUI>().text = Data[Index];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)){
            if (Index == 7){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(1);
                control.pause = false;
                hide();
                TimerStart = true;
            }
            if (Index == 18){
                control.SpawnDummy();
            }
            if ((Index <= 18 || Next) && Index != 21){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(1);
                Index++;
                Text.GetComponent<TextMeshProUGUI>().text = Data[Index];
            }
            if (Index == 21){
                GameObject.Find("EffectPlayer").GetComponent<EffectControl>().Play(3);
                Cover.Started = true;
            }
            
        }
        if (TimerStart){
            Timer++;
        }
        if (Timer == 300){
            //Index 7
            TimerStart = false;
            appear();
        }
    }
}
